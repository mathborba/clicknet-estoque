using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Engefibra.Data.Models;
using Engefibra.Data.Context;
using Engefibra.Web.Framework.Session;

namespace Engefibra.Web.Controllers
{
    public class ProdutoController : BaseController
    {
        private AppContext db = new AppContext();

        public ActionResult Index()
        {
            var model = db.Produto.ToList();
            var viewModel = new List<ViewModels.ProdutoViewModel>();

            foreach(var item in model)
            {
                viewModel.Add(new ViewModels.ProdutoViewModel
                {
                    Produto = item,
                    Movimentacoes = Bll.EstoqueMovimento.GetMovimentacaoProduto(item.Id),
                    SaldoProduto = Bll.EstoqueMovimento.GetSaldoAtualProduto(item.Id)
                });
            }

            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = db.Produto.Find(id);
            var viewModel = new ViewModels.ProdutoViewModel();
            viewModel.Produto = produto;
            viewModel.Movimentacoes = Bll.EstoqueMovimento.GetMovimentacaoProduto(produto.Id);
            viewModel.SaldoProduto = Bll.EstoqueMovimento.GetSaldoAtualProduto(produto.Id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.Produto();
            if(id > 0)
            {
                model = db.Produto.Where(x => x.Id == id).FirstOrDefault();
            }
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate([Bind(Include = "Id,Nome,Marca,Fornecedor,Observacao,Ativo,DataCriacao,UsuarioCriacao")] Produto model)
        {
            if (model.Id > 0)
            {
                model.DataAlteracao = DateTime.Now;
                model.UsuarioAlteracao = SessionManager.Current.ID;

                if (ModelState.IsValid)
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                model.DataCriacao = DateTime.Now;
                model.DataAlteracao = DateTime.Now;
                model.UsuarioAlteracao = SessionManager.Current.ID;
                model.UsuarioCriacao = SessionManager.Current.ID;

                if (ModelState.IsValid)
                {
                    db.Produto.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = db.Produto.Find(id);

            if (produto == null)
            {
                return HttpNotFound();
            }

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produto.Find(id);
            db.Produto.Remove(produto);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

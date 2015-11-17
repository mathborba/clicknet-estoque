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

namespace Engefibra.Web.Controllers
{
    public class EstoqueMovimentoController : BaseController
    {
        /// <summary>
        /// Retorna a listagem de movimentações realizadas no sistema
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = Bll.EstoqueMovimento.GetAll(true);
            return View(model);
        }

        /// <summary>
        /// Retorna uma view para criação da movimentação
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.EstoqueId = new SelectList(Bll.Estoque.GetAll(), "Id", "Nome");
            ViewBag.ProdutoId = new SelectList(Bll.Produto.GetAll(), "Id", "Nome");
            return View();
        }


        /// <summary>
        /// Criar uma movimentação de estoque para um produto
        /// </summary>
        /// <param name="model">Dados da movimentação</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,EstoqueId,MovimentoTipo,ProdutoId,Quantidade,Ativo")] EstoqueMovimento model)
        {
            model.DataCriacao = DateTime.Now;
            model.DataAlteracao = DateTime.Now;

            if (ModelState.IsValid)
            {
                model.Ativo = true;
                Bll.EstoqueMovimento.Add(model);
                return RedirectToAction("Index");
            }

            ViewBag.EstoqueId = new SelectList(Bll.Estoque.GetAll(), "Id", "Nome", model.EstoqueId);
            ViewBag.ProdutoId = new SelectList(Bll.Produto.GetAll(), "Id", "Nome", model.ProdutoId);

            return View(model);
        }

        /// <summary>
        /// Tela para confirmar se irá deletar mesmo ou não a movimentação
        /// </summary>
        /// <param name="id">Identificador da movimentação</param>
        /// <returns></returns>
        [Filters.Access(false, "Administrador,Encarregado,Lider")]
        public ActionResult Delete(int id)
        {
            var model = Bll.EstoqueMovimento.Get(id);
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        /// <summary>
        /// Deleta a movimentação caso seja confirmada.
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bll.EstoqueMovimento.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

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
        private AppContext db = new AppContext();

        // GET: /EstoqueMovimento/
        public ActionResult Index()
        {
            var estoquemovimento = db.EstoqueMovimento.Include(e => e.Estoque).Include(e => e.Produto);
            return View(estoquemovimento.ToList());
        }

        // GET: /EstoqueMovimento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstoqueMovimento estoquemovimento = db.EstoqueMovimento.Find(id);
            if (estoquemovimento == null)
            {
                return HttpNotFound();
            }
            return View(estoquemovimento);
        }

        // GET: /EstoqueMovimento/Create
        public ActionResult Create()
        {
            ViewBag.EstoqueId = new SelectList(db.Estoque, "Id", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produto, "Id", "Nome");
            return View();
        }

        // POST: /EstoqueMovimento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,EstoqueId,MovimentoTipo,ProdutoId,Quantidade,Ativo")] EstoqueMovimento estoquemovimento)
        {
            estoquemovimento.DataCriacao = DateTime.Now;
            estoquemovimento.DataAlteracao = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.EstoqueMovimento.Add(estoquemovimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstoqueId = new SelectList(db.Estoque, "Id", "Nome", estoquemovimento.EstoqueId);
            ViewBag.ProdutoId = new SelectList(db.Produto, "Id", "Nome", estoquemovimento.ProdutoId);

            return View(estoquemovimento);
        }

        // GET: /EstoqueMovimento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstoqueMovimento estoquemovimento = db.EstoqueMovimento.Find(id);
            if (estoquemovimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstoqueId = new SelectList(db.Estoque, "Id", "Nome", estoquemovimento.EstoqueId);
            ViewBag.ProdutoId = new SelectList(db.Produto, "Id", "Nome", estoquemovimento.ProdutoId);
            return View(estoquemovimento);
        }

        // POST: /EstoqueMovimento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,EstoqueId,MovimentoTipo,ProdutoId,Quantidade,Ativo,DataCriacao,DataAlteracao")] EstoqueMovimento estoquemovimento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estoquemovimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstoqueId = new SelectList(db.Estoque, "Id", "Nome", estoquemovimento.EstoqueId);
            ViewBag.ProdutoId = new SelectList(db.Produto, "Id", "Nome", estoquemovimento.ProdutoId);
            return View(estoquemovimento);
        }

        // GET: /EstoqueMovimento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstoqueMovimento estoquemovimento = db.EstoqueMovimento.Find(id);
            if (estoquemovimento == null)
            {
                return HttpNotFound();
            }
            return View(estoquemovimento);
        }

        // POST: /EstoqueMovimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstoqueMovimento estoquemovimento = db.EstoqueMovimento.Find(id);
            db.EstoqueMovimento.Remove(estoquemovimento);
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

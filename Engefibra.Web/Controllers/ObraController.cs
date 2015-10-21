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
    public class ObraController : BaseController
    {
        private AppContext db = new AppContext();

        // GET: /Obra/
        public ActionResult Index()
        {
            var obra = db.Obra.Include(o => o.Notificacao).Include(o => o.ObraStatus);
            return View(obra.ToList());
        }

        // GET: /Obra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }

        // GET: /Obra/Create
        public ActionResult Create()
        {
            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome");
            ViewBag.ObraStatusId = new SelectList(db.ObraStatus, "Id", "Nome");
            return View();
        }

        // POST: /Obra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nome,Endereco,Cep,Cidade,ObraStatusId,Observacao,Pendencias,NotificacaoId,Ativo,DataCriacao,DataAlteracao")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                db.Obra.Add(obra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome", obra.NotificacaoId);
            ViewBag.ObraStatusId = new SelectList(db.ObraStatus, "Id", "Nome", obra.ObraStatusId);
            return View(obra);
        }

        // GET: /Obra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome", obra.NotificacaoId);
            ViewBag.ObraStatusId = new SelectList(db.ObraStatus, "Id", "Nome", obra.ObraStatusId);
            return View(obra);
        }

        // POST: /Obra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome,Endereco,Cep,Cidade,ObraStatusId,Observacao,Pendencias,NotificacaoId,Ativo,DataCriacao,DataAlteracao")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome", obra.NotificacaoId);
            ViewBag.ObraStatusId = new SelectList(db.ObraStatus, "Id", "Nome", obra.ObraStatusId);
            return View(obra);
        }

        // GET: /Obra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obra.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }

        // POST: /Obra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Obra obra = db.Obra.Find(id);
            db.Obra.Remove(obra);
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

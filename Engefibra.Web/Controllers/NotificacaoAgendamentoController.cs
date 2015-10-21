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
    public class NotificacaoAgendamentoController : BaseController
    {
        private AppContext db = new AppContext();

        // GET: /NotificacaoAgendamento/
        public ActionResult Index()
        {
            var notificacaoagendamento = db.NotificacaoAgendamento.Include(n => n.Notificacao);
            return View(notificacaoagendamento.ToList());
        }

        // GET: /NotificacaoAgendamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificacaoAgendamento notificacaoagendamento = db.NotificacaoAgendamento.Find(id);
            if (notificacaoagendamento == null)
            {
                return HttpNotFound();
            }
            return View(notificacaoagendamento);
        }

        // GET: /NotificacaoAgendamento/Create
        public ActionResult Create()
        {
            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome");
            return View();
        }

        // POST: /NotificacaoAgendamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,NotificacaoId,AgendamentoHoras,AgendamentoTempo,Enviado")] NotificacaoAgendamento notificacaoagendamento)
        {
            if (ModelState.IsValid)
            {
                db.NotificacaoAgendamento.Add(notificacaoagendamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome", notificacaoagendamento.NotificacaoId);
            return View(notificacaoagendamento);
        }

        // GET: /NotificacaoAgendamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificacaoAgendamento notificacaoagendamento = db.NotificacaoAgendamento.Find(id);
            if (notificacaoagendamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome", notificacaoagendamento.NotificacaoId);
            return View(notificacaoagendamento);
        }

        // POST: /NotificacaoAgendamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,NotificacaoId,AgendamentoHoras,AgendamentoTempo,Enviado")] NotificacaoAgendamento notificacaoagendamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notificacaoagendamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NotificacaoId = new SelectList(db.Notificacao, "Id", "Nome", notificacaoagendamento.NotificacaoId);
            return View(notificacaoagendamento);
        }

        // GET: /NotificacaoAgendamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotificacaoAgendamento notificacaoagendamento = db.NotificacaoAgendamento.Find(id);
            if (notificacaoagendamento == null)
            {
                return HttpNotFound();
            }
            return View(notificacaoagendamento);
        }

        // POST: /NotificacaoAgendamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NotificacaoAgendamento notificacaoagendamento = db.NotificacaoAgendamento.Find(id);
            db.NotificacaoAgendamento.Remove(notificacaoagendamento);
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

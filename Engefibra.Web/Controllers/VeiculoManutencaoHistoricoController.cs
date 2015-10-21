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
    public class VeiculoManutencaoHistoricoController : BaseController
    {
        private AppContext db = new AppContext();

        // GET: /VeiculoManutencaoHistorico/
        public ActionResult Index()
        {
            return View(db.VeiculoManutencaoHistorico.ToList());
        }

        // GET: /VeiculoManutencaoHistorico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeiculoManutencaoHistorico veiculomanutencaohistorico = db.VeiculoManutencaoHistorico.Find(id);
            if (veiculomanutencaohistorico == null)
            {
                return HttpNotFound();
            }
            return View(veiculomanutencaohistorico);
        }

        // GET: /VeiculoManutencaoHistorico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /VeiculoManutencaoHistorico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,VeiculoId,KmAtual,ManutencaoRealizada,DataConfirmacao,DataNotificacao")] VeiculoManutencaoHistorico veiculomanutencaohistorico)
        {
            if (ModelState.IsValid)
            {
                db.VeiculoManutencaoHistorico.Add(veiculomanutencaohistorico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(veiculomanutencaohistorico);
        }

        // GET: /VeiculoManutencaoHistorico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeiculoManutencaoHistorico veiculomanutencaohistorico = db.VeiculoManutencaoHistorico.Find(id);
            if (veiculomanutencaohistorico == null)
            {
                return HttpNotFound();
            }
            return View(veiculomanutencaohistorico);
        }

        // POST: /VeiculoManutencaoHistorico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,VeiculoId,KmAtual,ManutencaoRealizada,DataConfirmacao,DataNotificacao")] VeiculoManutencaoHistorico veiculomanutencaohistorico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veiculomanutencaohistorico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(veiculomanutencaohistorico);
        }

        // GET: /VeiculoManutencaoHistorico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeiculoManutencaoHistorico veiculomanutencaohistorico = db.VeiculoManutencaoHistorico.Find(id);
            if (veiculomanutencaohistorico == null)
            {
                return HttpNotFound();
            }
            return View(veiculomanutencaohistorico);
        }

        // POST: /VeiculoManutencaoHistorico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VeiculoManutencaoHistorico veiculomanutencaohistorico = db.VeiculoManutencaoHistorico.Find(id);
            db.VeiculoManutencaoHistorico.Remove(veiculomanutencaohistorico);
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

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
    public class VeiculoController : BaseController
    {
        private AppContext db = new AppContext();

        // GET: /Veiculo/
        public ActionResult Index()
        {
            var veiculo = db.Veiculo.Include(v => v.VeiculoAlerta);
            return View(veiculo.ToList());
        }

        // GET: /Veiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = db.Veiculo.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // GET: /Veiculo/Create
        public ActionResult Create()
        {
            ViewBag.VeiculoAlertaId = new SelectList(db.VeiculoAlertas, "Id", "Nome");
            return View();
        }

        // POST: /Veiculo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Modelo,AnoFabricacao,Cor,Marca,KmInicial,AlertarManutencao,AlertarTrocaOleo,VeiculoAlertaId")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                db.Veiculo.Add(veiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VeiculoAlertaId = new SelectList(db.VeiculoAlertas, "Id", "Nome", veiculo.VeiculoAlertaId);
            return View(veiculo);
        }

        // GET: /Veiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = db.Veiculo.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.VeiculoAlertaId = new SelectList(db.VeiculoAlertas, "Id", "Nome", veiculo.VeiculoAlertaId);
            return View(veiculo);
        }

        // POST: /Veiculo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Modelo,AnoFabricacao,Cor,Marca,KmInicial,AlertarManutencao,AlertarTrocaOleo,VeiculoAlertaId")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VeiculoAlertaId = new SelectList(db.VeiculoAlertas, "Id", "Nome", veiculo.VeiculoAlertaId);
            return View(veiculo);
        }

        // GET: /Veiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = db.Veiculo.Find(id);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // POST: /Veiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Veiculo veiculo = db.Veiculo.Find(id);
            db.Veiculo.Remove(veiculo);
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

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
    public class VeiculoAlertaController : BaseController
    {
        private AppContext db = new AppContext();

        // GET: /VeiculoAlerta/
        public ActionResult Index()
        {
            return View(db.VeiculoAlertas.ToList());
        }

        // GET: /VeiculoAlerta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeiculoAlerta veiculoalerta = db.VeiculoAlertas.Find(id);
            if (veiculoalerta == null)
            {
                return HttpNotFound();
            }
            return View(veiculoalerta);
        }

        // GET: /VeiculoAlerta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /VeiculoAlerta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nome,KmOleo,KmManutencao,Ativo")] VeiculoAlerta veiculoalerta)
        {
            if (ModelState.IsValid)
            {
                db.VeiculoAlertas.Add(veiculoalerta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(veiculoalerta);
        }

        // GET: /VeiculoAlerta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeiculoAlerta veiculoalerta = db.VeiculoAlertas.Find(id);
            if (veiculoalerta == null)
            {
                return HttpNotFound();
            }
            return View(veiculoalerta);
        }

        // POST: /VeiculoAlerta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome,KmOleo,KmManutencao,Ativo")] VeiculoAlerta veiculoalerta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veiculoalerta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(veiculoalerta);
        }

        // GET: /VeiculoAlerta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeiculoAlerta veiculoalerta = db.VeiculoAlertas.Find(id);
            if (veiculoalerta == null)
            {
                return HttpNotFound();
            }
            return View(veiculoalerta);
        }

        // POST: /VeiculoAlerta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VeiculoAlerta veiculoalerta = db.VeiculoAlertas.Find(id);
            db.VeiculoAlertas.Remove(veiculoalerta);
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

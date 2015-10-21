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
    public class VeiculoUtilizacaoController : BaseController
    {
        private AppContext db = new AppContext();

        // GET: /VeiculoUtilizacao/
        public ActionResult Index()
        {
            var veiculoutilizacao = db.VeiculoUtilizacao.Include(v => v.Veiculo);
            return View(veiculoutilizacao.ToList());
        }

        // GET: /VeiculoUtilizacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeiculoUtilizacao veiculoutilizacao = db.VeiculoUtilizacao.Find(id);
            if (veiculoutilizacao == null)
            {
                return HttpNotFound();
            }
            return View(veiculoutilizacao);
        }

        // GET: /VeiculoUtilizacao/Create
        public ActionResult Create()
        {
            ViewBag.VeiculoId = new SelectList(db.Veiculo, "Id", "Modelo");
            return View();
        }

        // POST: /VeiculoUtilizacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,VeiculoId,NomeUtilizador,Observacao,KmFinal,DataCriacao")] VeiculoUtilizacao veiculoutilizacao)
        {
            if (ModelState.IsValid)
            {
                db.VeiculoUtilizacao.Add(veiculoutilizacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VeiculoId = new SelectList(db.Veiculo, "Id", "Modelo", veiculoutilizacao.VeiculoId);
            return View(veiculoutilizacao);
        }

        // GET: /VeiculoUtilizacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeiculoUtilizacao veiculoutilizacao = db.VeiculoUtilizacao.Find(id);
            if (veiculoutilizacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.VeiculoId = new SelectList(db.Veiculo, "Id", "Modelo", veiculoutilizacao.VeiculoId);
            return View(veiculoutilizacao);
        }

        // POST: /VeiculoUtilizacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,VeiculoId,NomeUtilizador,Observacao,KmFinal,DataCriacao")] VeiculoUtilizacao veiculoutilizacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veiculoutilizacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VeiculoId = new SelectList(db.Veiculo, "Id", "Modelo", veiculoutilizacao.VeiculoId);
            return View(veiculoutilizacao);
        }

        // GET: /VeiculoUtilizacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeiculoUtilizacao veiculoutilizacao = db.VeiculoUtilizacao.Find(id);
            if (veiculoutilizacao == null)
            {
                return HttpNotFound();
            }
            return View(veiculoutilizacao);
        }

        // POST: /VeiculoUtilizacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VeiculoUtilizacao veiculoutilizacao = db.VeiculoUtilizacao.Find(id);
            db.VeiculoUtilizacao.Remove(veiculoutilizacao);
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

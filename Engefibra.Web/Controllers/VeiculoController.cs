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

        public ActionResult Index()
        {
            var veiculo = db.Veiculo.Include(v => v.VeiculoAlerta);
            return View(veiculo.ToList());
        }

        public ActionResult Details(int id)
        {
            Veiculo veiculo = Bll.Veiculo.Get(id);

            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.Veiculo();

            if (id > 0)
            {
                model = Bll.Veiculo.Get(id);
            }

            ViewBag.VeiculoAlertaId = new SelectList(db.VeiculoAlertas, "Id", "Nome", model.VeiculoAlertaId);
            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate([Bind(Include="Id,Placa,Modelo,AnoFabricacao,Cor,Marca,KmInicial,Observacao,AlertarManutencao,AlertarTrocaOleo,VeiculoAlertaId")] Veiculo veiculo)
        {
            if (veiculo.Id > 0)
            {
                var utilizacaoAtiva = db.VeiculoUtilizacao.Where(x => x.VeiculoId == veiculo.Id && x.VeiculoUtilizacaoStatusId != 3).Count();

                if (utilizacaoAtiva > 0)
                {
                    ModelState.AddModelError("Placa", "Você não deletar um veiculo que está sendo utilizado por alguem.");
                }

                if (ModelState.IsValid)
                {
                    db.Entry(veiculo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Veiculo.Add(veiculo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.VeiculoAlertaId = new SelectList(db.VeiculoAlertas, "Id", "Nome", veiculo.VeiculoAlertaId);
            return View("Create", veiculo);
        }

        public ActionResult Delete(int id)
        {
            Veiculo veiculo = Bll.Veiculo.Get(id);

            if (veiculo == null)
            {
                return HttpNotFound();
            }

            return View(veiculo);
        }

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

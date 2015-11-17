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
    public class VeiculoUtilizacaoController : Controller
    {
        private AppContext db = new AppContext();

        public ActionResult Index()
        {
            var veiculoutilizacao = db.VeiculoUtilizacao.Include(v => v.Pessoa).Include(v => v.Veiculo).Include(v => v.VeiculoUtilizacaoStatus);
            return View(veiculoutilizacao.ToList());
        }

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

        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.VeiculoUtilizacao();

            if (id > 0) 
            {
                model = db.VeiculoUtilizacao
                    .Include(v => v.Pessoa)
                    .Include(v => v.Veiculo)
                    .Include(v => v.VeiculoUtilizacaoStatus)
                    .Where(v => v.Id == id).FirstOrDefault();

                var veiculoQuilometragemAtual = db.VeiculoUtilizacao.Where(x => x.VeiculoId == model.VeiculoId && x.VeiculoUtilizacaoStatusId == 3).OrderByDescending(c => c.Id).FirstOrDefault();
                model.Veiculo.KmAtual = veiculoQuilometragemAtual.KmFinal;

                ViewBag.PessoaId = new SelectList(db.Pessoa.Where(p => p.Id == model.PessoaId).ToList(), "Id", "Nome", model.PessoaId);
                ViewBag.VeiculoId = new SelectList(db.Veiculo.Where(p => p.Id == model.VeiculoId), "Id", "Placa", model.VeiculoId);
                ViewBag.VeiculoUtilizacaoStatusId = new SelectList(db.VeiculoUtilizacaoStatus.Where(p => p.Id != 1).ToList(), "Id", "Nome", model.VeiculoUtilizacaoStatusId);
            }
            else
            {
                ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", model.PessoaId);
                ViewBag.VeiculoId = new SelectList(db.Veiculo, "Id", "Placa", model.VeiculoId);
                ViewBag.VeiculoUtilizacaoStatusId = new SelectList(db.VeiculoUtilizacaoStatus, "Id", "Nome", model.VeiculoUtilizacaoStatusId);
            }

            var usuario = Bll.Usuario.Get(SessionManager.Current.ID);
            var utilizacaoAtiva = db.VeiculoUtilizacao.Where(v => (v.VeiculoUtilizacaoStatusId == 2 | v.VeiculoUtilizacaoStatusId == 1) 
                && v.PessoaId == usuario.PessoaId).Count();

            if (utilizacaoAtiva > 0 && model.Id == 0)
                model.ExisteUtilizacao = true;

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate([Bind(Include = "Id,VeiculoId,PessoaId,Observacao,KmFinal,Abastecimento,VeiculoUtilizacaoStatusId,DataCriacao,DataAlteracao,UsuarioCriacao,UsuarioAlteracao")] VeiculoUtilizacao veiculoutilizacao)
        {
            if (veiculoutilizacao.Id > 0)
            {
                veiculoutilizacao.DataAlteracao = DateTime.Now;
                veiculoutilizacao.UsuarioAlteracao = SessionManager.Current.ID;

                var ultimaUtilizacao = db.VeiculoUtilizacao.Where(x => x.VeiculoId == veiculoutilizacao.VeiculoId && x.VeiculoUtilizacaoStatusId == 3).OrderByDescending(c => c.Id).FirstOrDefault();
                var kmAtualVeiculo = ultimaUtilizacao.KmFinal;

                if (veiculoutilizacao.KmFinal <= kmAtualVeiculo)
                {
                    ModelState.AddModelError("KmFinal", "Você não pode inserir uma quilometragem menor do que a inicial");
                }

                if (ModelState.IsValid)
                {
                    db.Entry(veiculoutilizacao).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                veiculoutilizacao.DataAlteracao = DateTime.Now;
                veiculoutilizacao.UsuarioAlteracao = SessionManager.Current.ID;
                veiculoutilizacao.DataCriacao = DateTime.Now;
                veiculoutilizacao.UsuarioCriacao = SessionManager.Current.ID;

                if (ModelState.IsValid)
                {
                    db.VeiculoUtilizacao.Add(veiculoutilizacao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var id = veiculoutilizacao.Id;
            veiculoutilizacao = db.VeiculoUtilizacao
                    .Include(v => v.Pessoa)
                    .Include(v => v.Veiculo)
                    .Include(v => v.VeiculoUtilizacaoStatus)
                    .Where(v => v.Id == id).FirstOrDefault();

            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", veiculoutilizacao.PessoaId);
            ViewBag.VeiculoId = new SelectList(db.Veiculo, "Id", "Modelo", veiculoutilizacao.VeiculoId);
            ViewBag.VeiculoUtilizacaoStatusId = new SelectList(db.VeiculoUtilizacaoStatus, "Id", "Nome", veiculoutilizacao.VeiculoUtilizacaoStatusId);
            return View("Create", veiculoutilizacao);
        }

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

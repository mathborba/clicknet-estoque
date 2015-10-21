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
    public class PessoaController : Controller
    {
        private AppContext db = new AppContext();

        public ActionResult Index()
        {
            var pessoa = db.Pessoa.Include(p => p.PessoaTipo);
            return View(pessoa.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.Pessoa();

            if(id > 0)
            {
                model = db.Pessoa.Include(p => p.PessoaTipo).Where(x => x.Id == id).FirstOrDefault();
            }

            ViewBag.PessoaTipoId = new SelectList(db.PessoaTipo, "Id", "Nome");
            return View("Create", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate([Bind(Include = "Id,Nome,Sobrenome,PessoaFisica,RgIE,CpfCnpj,PessoaTipoId,ObraNotificacao,VeiculoNotificacao")] Pessoa model)
        {
            if (model.Id > 0)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Pessoa.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.PessoaTipoId = new SelectList(db.PessoaTipo, "Id", "Nome", model.PessoaTipoId);
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoa.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = db.Pessoa.Find(id);
            db.Pessoa.Remove(pessoa);
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

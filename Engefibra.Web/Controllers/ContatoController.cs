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
    public class ContatoController : BaseController
    {
        private AppContext db = new AppContext();

        public ActionResult Index()
        {
            return View(db.Contato.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contato contato = db.Contato.Find(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.Contato();
            if(id > 0)
            {
                model = db.Contato.Where(x => x.Id == id).FirstOrDefault();
            }

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate([Bind(Include="Id,Nome,Referencia,Telefone,Email,Observacao,Ativo,DataCriacao")] Contato contato)
        {
            if (contato.Id > 0)
            {
                contato.DataAlteracao = DateTime.Now;
                contato.UsuarioAlteracao = SessionManager.Current.ID;

                if (ModelState.IsValid)
                {
                    db.Entry(contato).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            else
            {
                contato.DataCriacao = DateTime.Now;
                contato.DataAlteracao = DateTime.Now;
                contato.UsuarioCriacao = SessionManager.Current.ID;
                contato.UsuarioAlteracao = SessionManager.Current.ID;

                if (ModelState.IsValid)
                {
                    db.Contato.Add(contato);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View("Create", contato);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contato contato = db.Contato.Find(id);

            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contato contato = db.Contato.Find(id);
            db.Contato.Remove(contato);
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

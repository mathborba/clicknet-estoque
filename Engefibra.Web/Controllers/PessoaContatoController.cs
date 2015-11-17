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
    public class PessoaContatoController : Controller
    {
        private AppContext db = new AppContext();

        // GET: /PessoaContato/
        public ActionResult Index(int pessoaId)
        {
            var pessoacontato = db.PessoaContato.Include(p => p.Pessoa).Where(x => x.PessoaId == pessoaId);
            return View(pessoacontato.ToList());
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.PessoaContato();
            if(id > 0)
            {
                model = db.PessoaContato.Include(p => p.Pessoa).Where(x => x.Id == id).FirstOrDefault();
            }

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate([Bind(Include = "Id,PessoaId,Nome,Email,TelefoneFixo,Ramal,Celular")] PessoaContato model)
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
                    db.PessoaContato.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", model.PessoaId);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            PessoaContato model = db.PessoaContato.Find(id);
            db.PessoaContato.Remove(model);
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

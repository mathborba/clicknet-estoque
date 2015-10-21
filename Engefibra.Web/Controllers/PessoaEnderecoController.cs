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
    public class PessoaEnderecoController : Controller
    {
        private AppContext db = new AppContext();

        public ActionResult Index(int pessoaId)
        {
            var pessoaendereco = db.PessoaEndereco.Include(p => p.Pessoa).Where(x => x.PessoaId == pessoaId);
            return View(pessoaendereco.ToList());
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.PessoaEndereco();

            if(id > 0)
            {
                model = db.PessoaEndereco.Include(p => p.Pessoa).Where(x => x.Id == id).FirstOrDefault();
            }

            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome");

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate([Bind(Include = "Id,PessoaId,Referencia,CEP,Logradouro,Numero,Cidade,UF")] PessoaEndereco model)
        {
            if(model.Id > 0)
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
                    db.PessoaEndereco.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.PessoaId = new SelectList(db.Pessoa, "Id", "Nome", model.PessoaId);
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            PessoaEndereco model = db.PessoaEndereco.Find(id);
            db.PessoaEndereco.Remove(model);
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

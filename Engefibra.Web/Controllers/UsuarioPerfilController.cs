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
    public class UsuarioPerfilController : BaseController
    {
        private AppContext db = new AppContext();

        public ActionResult Index()
        {
            var model = db.UsuarioPerfil
                .Include(u => u.Perfil)
                .Include(u => u.Usuario)
                .Include(u => u.Usuario.Pessoa);

            return View(model.ToList());
        }

        public ActionResult Create()
        {
            var perfilList = db.Perfil.Where(x => x.Ativo == true).ToList();
            var usuarioList = db.Usuario.Include("Pessoa").Where(x => x.Ativo == true).ToList();

            ViewBag.PerfilId = new SelectList(perfilList, "Id", "Nome");
            ViewBag.UsuarioId = new SelectList(usuarioList, "Id", "Pessoa.Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UsuarioId,PerfilId")] UsuarioPerfil model)
        {
            if (ModelState.IsValid)
            {
                db.UsuarioPerfil.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var perfilList = db.Perfil.Where(x => x.Ativo == true).ToList();
            var usuarioList = db.Usuario.Include("Pessoa").Where(x => x.Ativo == true).ToList();

            ViewBag.PerfilId = new SelectList(perfilList, "Id", "Nome", model.PerfilId);
            ViewBag.UsuarioId = new SelectList(usuarioList, "Id", "Pessoa.Nome", model.UsuarioId);

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsuarioPerfil model = db.UsuarioPerfil.Include(x => x.Usuario)
                                           .Include(x => x.Perfil)
                                           .Include(x => x.Usuario.Pessoa).Where(x => x.Id == id).FirstOrDefault();

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioPerfil model = db.UsuarioPerfil.Find(id);

            db.UsuarioPerfil.Remove(model);
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

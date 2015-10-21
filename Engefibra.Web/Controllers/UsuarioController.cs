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
    public class UsuarioController : BaseController
    {
        private AppContext db = new AppContext();

        #region .: Auth :.
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string User, string Pass)
        {
            var verificarLogin = Web.Framework.Session.SessionManager.Logon(User, Pass);

            return Json(new { 
                sucesso = (verificarLogin != null) 
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            SessionManager.Logout();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        public ActionResult Index()
        {
            var model = Bll.Usuario.GetAll();
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = Bll.Usuario.Get(id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // GET: /Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nome,Sobrenome,Login,Senha,CpfCnpj,Ativo")] Usuario usuario)
        {
            usuario.DataCriacao = DateTime.Now;
            usuario.DataAlteracao = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: /Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: /Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome,Sobrenome,Login,Senha,CpfCnpj,Ativo,DataCriacao,DataAlteracao")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: /Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: /Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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

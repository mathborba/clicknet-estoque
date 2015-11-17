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

        public ActionResult Details(int id)
        {
            var model = Bll.Usuario.Get(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.Usuario();
            if(id > 0)
            {
                model = Bll.Usuario.Get(id);
            }
            ViewBag.PessoaId = new SelectList(Bll.Pessoa.GetAll(), "Id", "Nome", model.PessoaId);

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate([Bind(Include="Id,PessoaId,Nome,Sobrenome,Login,Senha,CpfCnpj,Ativo,DataCriacao,UsuarioCriacao")] Usuario model)
        {
            if(model.Id > 0)
            {
                model.DataAlteracao = DateTime.Now;
                model.UsuarioAlteracao = SessionManager.Current.ID;

                if (ModelState.IsValid)
                {
                    Bll.Usuario.Alter(model);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                model.DataCriacao = DateTime.Now;
                model.DataAlteracao = DateTime.Now;
                model.UsuarioCriacao = SessionManager.Current.ID;
                model.UsuarioAlteracao = SessionManager.Current.ID;

                if (ModelState.IsValid)
                {
                    Bll.Usuario.Add(model);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.PessoaId = new SelectList(Bll.Pessoa.GetAll(), "Id", "Nome", model.PessoaId);
            return View("Create", model);
        }

        public ActionResult Delete(int id)
        {
            Usuario usuario = Bll.Usuario.Get(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bll.Usuario.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MeusDados()
        {
            var usuario = Bll.Usuario.Get(SessionManager.Current.ID);
            return View(usuario);
        }

        [HttpGet]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarSenha(string senhaAntiga, string senhaNova, string senhaConfirmacao)
        {
            var usuario = Bll.Usuario.Get(SessionManager.Current.ID);

            if(senhaAntiga == usuario.Senha)
            {
                if(senhaNova == senhaConfirmacao)
                {
                    usuario.Senha = senhaNova;
                    Bll.Usuario.Alter(usuario);
                }
                else
                {
                    return Json(new { senhaAlterada = false, Erro = "Nova senha e a sua confirmação não conferem, por favor, verifique!" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { senhaAlterada = false, Erro = "Senha atual incorreta!" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { senhaAlterada = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmailDiretoria()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailDiretoria(string assunto, string corpo)
        {
            if(assunto.IsNotNull() || corpo.IsNotNull())
            {
                var nomeEnviado = SessionManager.Current.Name;
                Bll.Usuario.EmailDiretoria(nomeEnviado, assunto, corpo);

                return Json(new { emailEnviado = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { emailEnviado = false }, JsonRequestBehavior.AllowGet);
        }
    }
}

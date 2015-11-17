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
        /// <summary>
        /// Listagem de Contatos
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = Bll.Contato.GetAll();
            return View(model);
        }

        /// <summary>
        /// Detalhamento do contato
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Contato contato = Bll.Contato.Get(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        /// <summary>
        /// View para adição/edição de um contato
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        public ActionResult AddOrUpdate(int id = 0)
        {
            var model = new Data.Models.Contato();

            if(id > 0)
            {
                model = Bll.Contato.Get(id);
            }

            return View("Create", model);
        }

        /// <summary>
        /// Adiciona/edita um contato no sistema
        /// </summary>
        /// <param name="contato">Dados do contato.</param>
        /// <returns></returns>
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
                    Bll.Contato.Alter(contato);
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
                    Bll.Contato.Add(contato);
                    return RedirectToAction("Index");
                }
            }

            return View("Create", contato);
        }

        /// <summary>
        /// View de confirmação de remoção
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            Contato contato = Bll.Contato.Get(id);

            if (contato == null)
            {
                return HttpNotFound();
            }

            return View(contato);
        }

        /// <summary>
        /// Remover o contato
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bll.Contato.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

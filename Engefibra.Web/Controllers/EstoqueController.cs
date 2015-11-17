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
    public class EstoqueController : BaseController
    {
        /// <summary>
        /// Listagem dos estoques cadastrados
        /// </summary>
        /// <returns></returns>
        [Filters.Access(false, "Administrador,Almoxarifado")]
        public ActionResult Index()
        {
            var model = Bll.Estoque.GetAll();
            return View(model);
        }

        /// <summary>
        /// Detalhamento de estoque por identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var model = Bll.Estoque.Get(id);
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        /// <summary>
        /// Retorna uma view para criação de um estoque
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Criação de um estoque no sistema
        /// </summary>
        /// <param name="estoque">Dados do estoque.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nome,Ativo")] Estoque estoque)
        {
            estoque.DataCriacao = DateTime.Now;
            estoque.DataAlteracao = DateTime.Now;

            if (ModelState.IsValid)
            {
                Bll.Estoque.Add(estoque);
                return RedirectToAction("Index");
            }

            return View(estoque);
        }

        /// <summary>
        /// Retorna view para edição do anuncio
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var model = Bll.Estoque.Get(id);
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        /// <summary>
        /// Edita o estoque com os novos dados
        /// </summary>
        /// <param name="estoque">Estoque.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome,Ativo,DataCriacao,DataAlteracao")] Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                Bll.Estoque.Alter(estoque);
                return RedirectToAction("Index");
            }
            return View(estoque);
        }

        /// <summary>
        /// Retorna uma view para confirmação da remoção do estoque
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var model = Bll.Estoque.Get(id);

            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        /// <summary>
        /// Remove o estoque selecionado
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bll.Estoque.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

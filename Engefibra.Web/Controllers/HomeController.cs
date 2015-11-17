using Engefibra.Data.Context;
using Engefibra.Web.Framework.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Engefibra.Web.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Página inicial do sistema
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new ViewModels.HomeViewModel();

            model.Obras = Bll.Obra.GetAll(true).Take(5).ToList();
            model.VeiculoUtilizacao = Bll.VeiculoUtilizacao.GetAll(true).Take(5).ToList();
            model.Produtos = Bll.Produto.GetAll(true).Take(5).ToList();
            model.Movimentacoes = Bll.EstoqueMovimento.GetAll(true).Take(5).ToList();

            foreach(var item in model.Produtos)
            {
                item.SaldoAtual = Bll.EstoqueMovimento.GetSaldoAtualProduto(item.Id);
            }

            return View(model);
        }

        /// <summary>
        /// Método retornado quando o usuário não possui permissão para acessar alguma funcionalidade
        /// </summary>
        /// <returns></returns>
        public ActionResult PermissaoNegada()
        {
            ViewBag.UserName = SessionManager.Current.Name;
            ViewBag.DateDenied = DateTime.Now;
            return View("SemPermissao");
        }
    }
}
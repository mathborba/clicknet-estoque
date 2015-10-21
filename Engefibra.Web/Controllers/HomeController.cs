using Engefibra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Engefibra.Web.Controllers
{
    public class HomeController : BaseController
    {
        private AppContext db = new AppContext();

        public ActionResult Index()
        {
            var model = new ViewModels.HomeViewModel();

            model.Obras = db.Obra.OrderByDescending(o => o.DataCriacao).Take(5).ToList();
            model.Veiculo = db.Veiculo.OrderByDescending(o => o.Id).Take(5).ToList();
            model.Produtos = db.Produto.OrderByDescending(o => o.Id).Take(5).ToList();
            model.Movimentacoes = db.EstoqueMovimento.OrderByDescending(o => o.Id).Take(5).ToList();

            return View(model);
        }

        public ActionResult PermissaoNegada()
        {
            return View("SemPermissao");
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
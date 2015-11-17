using Engefibra.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Engefibra.Web.Controllers
{
    [Access(false)]
    public class BaseController : Controller
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public BaseController() { }

        /// <summary>
        /// Popula a propriedade pagina do request 
        /// </summary>
        /// <param name="filterContext">filtro do contexto da ação sendo executada</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Define as propriedades do layout antes da execuçao do result
        /// </summary>
        /// <param name="filterContext">filtro de contexto do result</param>
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;
            base.OnResultExecuting(filterContext);
        }

        /// <summary>
        /// Ao ocorrer uma exception na aplicação ela será tratada 
        /// </summary>
        /// <param name="filterContext">contexto de exception </param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        /// <summary>
        /// Retorna o HTML da view a ser renderizada 
        /// </summary>
        /// <param name="controller">Controller que irá renderizar o HTML</param>
        /// <param name="viewName">nome da view a ser renderizada</param>
        /// <param name="model">Classe model esperada pela View</param>
        /// <returns>O html renderizado com o HTML da view e o dados da Model</returns>
        public string GetViewHtml(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
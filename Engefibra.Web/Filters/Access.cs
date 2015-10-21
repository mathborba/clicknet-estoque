using Engefibra.Web.Framework.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Engefibra.Web.Filters
{
    public class Access : ActionFilterAttribute
    {
        private bool ActionPublic;
        private string[] PerfisParametro;

        public Access(bool actionPublic, string perfisParametro = "")
        {
            this.ActionPublic = actionPublic;
            var perfis = perfisParametro.Split(',');

            if(perfis.Count() > 0)
                this.PerfisParametro = perfis;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AccessControl.VerificaPermissao(filterContext, ActionPublic, PerfisParametro.ToList());
            base.OnActionExecuting(filterContext);
        }
    }

    public static class AccessControl
    {
        public static void VerificaPermissao(System.Web.Mvc.ActionExecutingContext filterContext, bool publicAction = false, List<string> perfis = null)
        {
            ActionDescriptor actionDescriptor = filterContext.ActionDescriptor;

            string actionName = actionDescriptor.ActionName;
            string controllerName = actionDescriptor.ControllerDescriptor.ControllerName;

            string[] actionPublics = { "Logout", "Login", "LogOn" };

            if (controllerName == "Usuario" && actionPublics.Contains(actionName)) 
                publicAction = true;

            if (publicAction) 
                return;

            if (!SessionManager.Current.Logged)
            {
                filterContext.Result = new RedirectResult(HttpContext.Current.Request.Url.Scheme + "://" +
                   HttpContext.Current.Request.Url.Authority +
                   HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/Usuario/Login");

                return;
            }

            if(perfis.Count() > 1)
            {
                bool perfilAutorizado = false;
                var userSessionPerfis = SessionManager.Current.Perfis;

                foreach(var item in userSessionPerfis)
                {
                    if (perfis.Contains(item))
                    {
                        perfilAutorizado = true;
                        break;
                    }
                }
                
                if(!perfilAutorizado)
                {
                    filterContext.Result = new RedirectResult(HttpContext.Current.Request.Url.Scheme + "://" +
                        HttpContext.Current.Request.Url.Authority +
                        HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/Home/PermissaoNegada");

                    return;
                }
            }
        }
    }
}
using Engefibra.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Engefibra.Web.Framework.Session
{
    public class SessionManager
    {
        const string NameCookie = "ENGEFIBRA_NAME";
        const string NameUserID = "ENGEFIBRA_GUID";
        const string NameID = "ENGEFIBRA_UNIQUE";
        const string NamePerfis = "ENGEFIBRA_ROLE";

        public static UserContext Current
        {
            get
            {
                var userContext = new UserContext
                {
                    Name = "",
                    ID = 0,
                    Logged = false,
                };

                var cookieName = HttpContext.Current.Request.Cookies[NameCookie];
                var cookieUserID = HttpContext.Current.Request.Cookies[NameUserID];
                var cookieID = HttpContext.Current.Request.Cookies[NameID];
                var cookiePerfis = HttpContext.Current.Request.Cookies[NamePerfis];

                if (cookieName != null && !string.IsNullOrEmpty(cookieName.Value)
                    && cookieUserID != null && !string.IsNullOrEmpty(cookieUserID.Value)
                    && cookieID != null && !string.IsNullOrEmpty(cookieID.Value))
                {
                    Guid guidCookieId = new Guid(cookieID.Value.Decrypt());
                    var userId = Convert.ToInt32(cookieUserID.Value.Decrypt());
                    var perfis = cookiePerfis.Value.ToString().Split(',').ToArray();

                    userContext.Name = cookieName.Value.Decrypt();
                    userContext.ID = userId;
                    userContext.CookieID = guidCookieId;
                    userContext.Logged = true;
                    userContext.Perfis = perfis.ToList();
                }

                userContext.IpAddress = HttpContext.Current.Request.UserHostAddress;
                return userContext;
            }
        }

        public static Data.Models.Usuario Logon(string usuario, string senha)
        {
            var u = Bll.Usuario.ValidateLogin(usuario, senha);
            var uPerfis = new List<string>();

            if (u != null)
            {
                uPerfis = Bll.UsuarioPerfil.GetCommaSeparatedUserRole(u.Id);

                HttpContext.Current.Response.Cookies[NameCookie].Value = u.Pessoa.Nome.Encrypt();
                HttpContext.Current.Response.Cookies[NameUserID].Value = u.Id.ToString().Encrypt();
                HttpContext.Current.Response.Cookies[NameID].Value = new Guid().ToString().Encrypt();
                HttpContext.Current.Response.Cookies[NamePerfis].Value = string.Join(",", uPerfis).Encrypt();

                HttpContext.Current.Response.Cookies[NameCookie].Expires = DateTime.Now.AddHours(4);
                HttpContext.Current.Response.Cookies[NameUserID].Expires = DateTime.Now.AddHours(4);
                HttpContext.Current.Response.Cookies[NameID].Expires = DateTime.Now.AddHours(4);
                HttpContext.Current.Response.Cookies[NamePerfis].Expires = DateTime.Now.AddHours(4);
            }

            return u;
        }

        public static void Logout()
        {
            var cookieID = HttpContext.Current.Request.Cookies[NameID];

            if (cookieID != null && cookieID.Value != null)
            {
                HttpContext.Current.Response.Cookies[cookieID.Value].Expires = DateTime.Now;
            }

            HttpContext.Current.Response.Cookies[NameCookie].Expires = DateTime.Now;
            HttpContext.Current.Response.Cookies[NameUserID].Expires = DateTime.Now;
            HttpContext.Current.Response.Cookies[NameID].Expires = DateTime.Now;
            HttpContext.Current.Response.Cookies[NamePerfis].Expires = DateTime.Now;
        }
    }
}

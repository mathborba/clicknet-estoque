using Engefibra.Web.Framework.Session;
using Hangfire.Dashboard;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engefibra.Web.Filters
{
    public class AcessHangfireFilter : IAuthorizationFilter
    {
        public bool Authorize(IDictionary<string, object> owinEnvironment)
        {
            bool authorized = false;

            var user = SessionManager.Current;

            if (user != null)
            {
                authorized = true;
            }

            return authorized;
        }
    }
}
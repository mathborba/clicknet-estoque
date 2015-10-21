using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Engefibra.Web.Startup))]

namespace Engefibra.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                GlobalConfiguration.Configuration
                    .UseSqlServerStorage("EngefibraAppContext");

                app.UseHangfireDashboard("/worker",
                    new DashboardOptions { AuthorizationFilters = new[] { new Filters.AcessHangfireFilter() } });

                app.UseHangfireServer();
            }
            catch
            {
                // Ele só deve ser executado depois que o Migrations rolar, ou seja, na segunda execução
            }
        }
    }
}

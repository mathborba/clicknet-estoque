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

                app.UseHangfireServer(new BackgroundJobServerOptions
                {
                    Queues = new[] { "obras", "veiculos", "sistema"}
                });

                // Load Hubs
                Jobs.VeiculosHub.LoadWorkers();
                Jobs.ObrasHub.LoadWorkers();
            }
            catch { }
        }
    }
}
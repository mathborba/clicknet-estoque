using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engefibra.Web.Jobs
{
    public static class ObrasHub
    {
        /// <summary>
        /// Carregar os jobs com a configuração de tempo de execução
        /// </summary>
        public static void LoadWorkers()
        {
            RecurringJob.AddOrUpdate("Obras_Agendamento", () => NotificarObrasAgendamento(), Cron.Daily);
            RecurringJob.AddOrUpdate("Obras_Pendencia", () => NotificarObrasPendencia(), Cron.Daily);
        }

        /// <summary>
        /// Notifica obras com agendamento
        /// </summary>
        [Queue("obras")]
        public static void NotificarObrasAgendamento()
        {
            Bll.Obra.NotificarObrasAgendadas();
        }

        /// <summary>
        /// Notficiar obras com pendência
        /// </summary>
        [Queue("obras")]
        public static void NotificarObrasPendencia()
        {
            Bll.Obra.NotificarObrasComPendencia();
        }
    }
}
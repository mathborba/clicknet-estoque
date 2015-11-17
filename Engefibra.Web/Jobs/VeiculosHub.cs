using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engefibra.Web.Jobs
{
    public static class VeiculosHub
    {
        /// <summary>
        /// Carregar os jobs com a configuração de tempo de execução
        /// </summary>
        public static void LoadWorkers()
        {
            RecurringJob.AddOrUpdate("Veiculos_ObservacaoEstado", () => NotificarPendentesObservacao(), Cron.Daily);
            RecurringJob.AddOrUpdate("Veiculos_ManutencaoPendente", () => NotificarPendentesManutencao(), Cron.Daily);
        }

        /// <summary>
        /// Notifica os veiculos que possuem pendências de observação de estado
        /// </summary>
        [Queue("veiculos")]
        public static void NotificarPendentesObservacao()
        {
            Bll.Veiculo.NotificarVeiculosObservacao();
        }

        /// <summary>
        /// Notifica os veiculos que possuem pendências de manutenção
        /// </summary>
        [Queue("veiculos")]
        public static void NotificarPendentesManutencao()
        {
            Bll.Veiculo.NotificarAlertasManutencao();
        }
    }
}
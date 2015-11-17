using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class Veiculo
    {
        #region .: CRUD :.
        public static Data.Models.Veiculo Get(int id)
        {
            var model = new Data.Models.Veiculo();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Veiculo.Include("VeiculoAlerta").Where(e => e.Id == id).FirstOrDefault();
                var veiculoQuilometragemAtual = db.VeiculoUtilizacao.Where(x => x.VeiculoId == model.Id && x.VeiculoUtilizacaoStatusId == 3).OrderByDescending(c => c.Id).FirstOrDefault();
                model.KmAtual = veiculoQuilometragemAtual.KmFinal;
            }
            return model;
        }

        public static List<Data.Models.Veiculo> GetAll(bool orderBy = false)
        {
            var model = new List<Data.Models.Veiculo>();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Veiculo.Include("VeiculoAlerta").ToList();

                foreach (var item in model)
                {
                    var veiculoQuilometragemAtual = db.VeiculoUtilizacao.Where(x => x.VeiculoId == item.Id && x.VeiculoUtilizacaoStatusId == 3).OrderByDescending(c => c.Id).FirstOrDefault();
                    item.KmAtual = veiculoQuilometragemAtual.KmFinal;
                }

                if (orderBy)
                    model = model.OrderByDescending(c => c.Id).ToList();
            }
            return model;
        }

        public static bool Add(Data.Models.Veiculo model)
        {
            bool sucesso = true;

            using (var db = new Data.Context.AppContext())
            {
                db.Veiculo.Add(model);
                db.SaveChanges();
            }
            return sucesso;
        }

        public static void Delete(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                Data.Models.Veiculo model = db.Veiculo.Find(id);
                db.Veiculo.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion

        /// <summary>
        /// Notificar todos os veiculos que possuem KmAtual maior do que a estabelecida para manutenção
        /// </summary>
        public static void NotificarAlertasManutencao()
        {
            using (var db = new Data.Context.AppContext())
            {
                var veiculos = GetAll();

                foreach (var item in veiculos)
                {
                    var ultimaManutencao = db.VeiculoManutencaoHistorico
                                             .Where(x => x.VeiculoId == item.Id && x.ManutencaoRealizada == true)
                                             .OrderByDescending(x => x.Id).FirstOrDefault();
                    var kmInicialConsideracao = (ultimaManutencao != null ? ultimaManutencao.KmAtual : item.KmInicial);

                    // Caso a Km Atual - Km Inicial for igual ou maior a estabelecida no alerta, toca lenha
                    var quilometragemManutencao = db.VeiculoAlertas.Where(c => c.Id == item.VeiculoAlertaId).FirstOrDefault();

                    if ((item.KmAtual - kmInicialConsideracao) >= quilometragemManutencao.KmManutencao)
                    {
                        var destinatarios = db.Pessoa.Where(x => x.VeiculoNotificacao == true).Select(x => x.Email).ToList();

                        var mailer = new Mailer();

                        mailer.assunto = "[Veiculo] Programar manutenção para: " + item.Placa + " - " + item.Modelo;
                        mailer.destinatarios = destinatarios;
                        mailer.tipoEnvioEmail = MailerSendType.Veiculos;

                        mailer.corpo =
                            "Foi constatado que o veiculo (" + item.Placa + " - " + item.Modelo + ") rodou (" + (item.KmAtual - item.KmInicial) + " kms) após a última manutenção, e foi chegado o momento de realizar uma nova manutenção no mesmo.<br /><br />" +
                            "<strong>Plano de manutenção escolhido:</strong> " + quilometragemManutencao.Nome + " - manutenção programada a cada " + quilometragemManutencao.KmManutencao + " kms.<br/>" +
                            "<strong>Quilometragem ultima manutenção:</strong> " + kmInicialConsideracao + "<br/>" +
                            "<strong>Quilometragem atual:</strong> " + item.KmAtual + "<br/>" +
                            "<strong>Dados veiculo:</strong> " + item.Placa + " - " + item.Modelo + ", " + item.Cor + "/" + item.AnoFabricacao + "<br/><br />" +
                            "Para não receber notificações, encerre a manutenção pendente no sistema, no menu <strong>Veiculos >> Histórico de manutenção de veiculos</strong>";
                        mailer.corpoHtml = true;

                        mailer.Send();

                        // Caso não possua nenhuma manutenção pendente, inserir, para aguardar a interação do administrador
                        if (db.VeiculoManutencaoHistorico.Where(x => x.VeiculoId == item.Id && x.ManutencaoRealizada == false).Count() == 0)
                        {
                            db.VeiculoManutencaoHistorico.Add(new Data.Models.VeiculoManutencaoHistorico
                            {
                                VeiculoId = item.Id,
                                KmAtual = item.KmAtual,
                                DataNotificacao = DateTime.Now,
                                DataConfirmacao = DateTime.Now,
                                ManutencaoRealizada = false,
                                UsuarioAlteracao = 0,
                                UsuarioCriacao = 0
                            });
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Notificar todos os veiculos que possuem observação referente a estado do Carro
        /// </summary>
        public static void NotificarVeiculosObservacao()
        {
            using (var db = new Data.Context.AppContext())
            {
                var veiculos = GetAll();
                foreach (var item in veiculos)
                {
                    if (!String.IsNullOrEmpty(item.Observacao))
                    {
                        var destinatarios = db.Pessoa.Where(x => x.VeiculoNotificacao == true).Select(x => x.Email).ToList();

                        var mailer = new Mailer();

                        mailer.assunto = "[Veiculo] Observação sobre estado: " + item.Placa + " - " + item.Modelo;
                        mailer.destinatarios = destinatarios;
                        mailer.tipoEnvioEmail = MailerSendType.Veiculos;

                        mailer.corpo =
                            "Foi constatado que o veiculo (" + item.Placa + " - " + item.Modelo + ") possui observações, quanto ao seu estado de conservação.<br /><br />" +
                            "<strong>Observação:</strong> " + item.Observacao + "<br/><br/>" +
                            "Caso as observações não sejam reais, ou o problema já tenha sido resolvido, vá até o veiculo no sistema, e remova a observação para não receber novas notificações!";
                        mailer.corpoHtml = true;

                        mailer.Send();
                    }
                }
            }
        }
    }
}
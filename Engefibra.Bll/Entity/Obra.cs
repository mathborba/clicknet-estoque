using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class Obra
    {
        #region .: CRUD :.
        public static Data.Models.Obra Get(int id)
        {
            var model = new Data.Models.Obra();

            using(var db = new Data.Context.AppContext())
            {
                model = db.Obra.Include("Cliente")
                               .Include("Encarregado")
                               .Include("ObraStatus")
                               .Include("Notificacao").Where(e => e.Id == id).FirstOrDefault();
            }
            return model;
        }

        public static List<Data.Models.Obra> GetAll(bool orderBy = false)
        {
            var model = new List<Data.Models.Obra>();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Obra.Include("Cliente")
                               .Include("Encarregado")
                               .Include("ObraStatus")
                               .Include("Notificacao").ToList();
                if (orderBy)
                    model = model.OrderByDescending(c => c.Id).ToList();
            }
            return model;
        }

        public static bool Add(Data.Models.Obra model)
        {
            bool sucesso = true;

            using (var db = new Data.Context.AppContext())
            {
                db.Obra.Add(model);
                db.SaveChanges();
            }
            return sucesso;
        }

        public static void Delete(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                Data.Models.Obra model = db.Obra.Find(id);
                db.Obra.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion

        /// <summary>
        /// Notificação sobre obras agendadas
        /// </summary>
        public static void NotificarObrasAgendadas()
        {
            var obrasAgendadas = GetAll().Where(x => x.ObraStatusId == 1).ToList();

            foreach (var item in obrasAgendadas)
            {
                var dataAtual = DateTime.Now;
                var dataMinimaNotificacao = item.DataAgendamento.AddDays(-2);
                var dataMaximaNotificacao = item.DataAgendamento.AddDays(2);

                if(dataAtual >= dataMinimaNotificacao && dataAtual <= dataMaximaNotificacao)
                {
                    using (var db = new Data.Context.AppContext())
                    {
                        var destinatarios = db.Pessoa.Where(x => x.ObraNotificacao == true).Select(x => x.Email).ToList();

                        var mailer = new Mailer();

                        mailer.assunto = "[Obra] " + item.Nome + ", agendada para " + item.DataAgendamento.ToString("dd/MM/yyyy") + " aguarda uma interação!";
                        mailer.destinatarios = destinatarios;
                        mailer.tipoEnvioEmail = MailerSendType.Obras;

                        mailer.corpo =
                            "A obra " + item.Nome + ", do cliente " + item.Cliente.Nome + " está em AGENDAMENTO para a data " + item.DataAgendamento.ToString("dd/MM/yyyy") + ".<br /><br />" +
                            "<strong>Encarregado:</strong> " + item.Encarregado.Nome + "<br/>" +
                            "<strong>Pendencias:</strong> " + item.Pendencias + "<br/>" +
                            "<strong>Observações:</strong> " + item.Observacao + "<br/>" +
                            "<strong>Endereço:</strong> " + item.Endereco + " - " + item.Cep + ", " + item.Cidade + "<br/><br />" +
                            "Para não receber notificações, atualize a situação da Obra, ou encerre a mesma caso não tiver outra opção.";
                        mailer.corpoHtml = true;

                        mailer.Send();

                        db.ObraNotificacao.Add(new Data.Models.ObraNotificacao
                        {
                            ObraId = item.Id,
                            Ativo = true,
                            DataCriacao = DateTime.Now,
                            DataAlteracao = DateTime.Now
                        });
                        db.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Notificação sobre obras com Situação "Pendência" ou Campo "Pendência" preenchido
        /// </summary>
        public static void NotificarObrasComPendencia()
        {
            var obrasPendencia = GetAll().Where(x => x.ObraStatusId == 3 || !String.IsNullOrEmpty(x.Pendencias)).ToList();
            foreach (var item in obrasPendencia)
            {
                using (var db = new Data.Context.AppContext())
                {
                    var destinatarios = db.Pessoa.Where(x => x.ObraNotificacao == true).Select(x => x.Email).ToList();

                    var mailer = new Mailer();

                    mailer.assunto = "[Obra] " + item.Nome + ", com situação PENDENCIA! [IMPORTANTE]";
                    mailer.destinatarios = destinatarios;
                    mailer.tipoEnvioEmail = MailerSendType.Obras;

                    mailer.corpo =
                        "A obra " + item.Nome + ", do cliente " + item.Cliente.Nome + " está com PENDENCIAS que precisam ser resolvidas para normalização da obra.<br /><br />" +
                        "<strong>Situação:</strong> " + item.ObraStatus.Nome + "<br/>" +
                        "<strong>Encarregado:</strong> " + item.Encarregado.Nome + "<br/>" +
                        "<strong>Pendencias:</strong> " + item.Pendencias + "<br/>" +
                        "<strong>Observações:</strong> " + item.Observacao + "<br/>" +
                        "<strong>Endereço:</strong> " + item.Endereco + " - " + item.Cep + ", " + item.Cidade + "<br/><br />" +
                        "Para não receber notificações, atualize a situação da Obra, ou encerre a mesma caso não tiver outra opção.";
                    mailer.corpoHtml = true;

                    mailer.Send();

                    db.ObraNotificacao.Add(new Data.Models.ObraNotificacao
                    {
                        ObraId = item.Id,
                        Ativo = true,
                        DataCriacao = DateTime.Now,
                        DataAlteracao = DateTime.Now
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
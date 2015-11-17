using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Engefibra.Bll
{
    public enum MailerSendType
    {
        Obras = 1, /* 0: Notificações sobre obras, adição de novas Obras, etc. */
        Veiculos = 2, /* 1: Notificações sobre veiculos, utilização do mesmo, manutenção, cadastro, etc. */
        Produtos = 3, /* 2: Notificações sobre situação critica de estoque, etc. */
        Sistema = 4, /* 3: Notificações sobre exceções, sobre quedas, relatórios gerais, etc. */
        Outros = 5 /* 4: Qualquer outro disparo não enquadrado na situação acima */
    }

    public class Mailer
    {
        #region .: Propriedades :.
        public List<string> destinatarios { get; set; }
        public string assunto { get; set; }
        public MailerSendType tipoEnvioEmail { get; set; }
        public bool corpoHtml { get; set; }
        public string corpo { get; set; }
        public bool enableSsl { get; set; }
        public string remetente { get; set; }
        public string remetenteSenha { get; set; }
        public string remetenteNome { get; set; }
        public string smtpServidor { get; set; }
        public int smtpPorta { get; set; }
        #endregion

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Mailer"/>.
        /// </summary>
        public Mailer()
        {
            // Inicialização das listas genéricas
            destinatarios = new List<string>();

            // Configuração do envio
            enableSsl       = Convert.ToBoolean(Configuration.GetValue("Mailer_Ssl"));
            remetente       = Configuration.GetValue("Mailer_Remetente").ToString();
            remetenteSenha  = Configuration.GetValue("Mailer_RemetenteSenha").ToString();
            remetenteNome   = Configuration.GetValue("Mailer_RemetenteNome").ToString();
            smtpServidor    = Configuration.GetValue("Mailer_EnderecoSmtp").ToString();
            smtpPorta       = Convert.ToInt32(Configuration.GetValue("Mailer_PortaSmtp"));
        }

        /// <summary>
        /// Gera um corpo para o e-mail enviado, baseado nos dados passados.
        /// </summary>
        /// <param name="templateName">Nome do template.</param>
        /// <param name="destinatario">Destinatario que irá receber a mensagem.</param>
        /// <param name="conteudo">Conteudo</param>
        /// <returns></returns>
        public string GenerateBodyFromTemplate(string templateName, string destinatario, string conteudo)
        {
            string body = string.Empty;

            // Ler o template baseado em um template Html
            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Email/" + templateName + ".html", Encoding.GetEncoding("ISO-8859-1")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{{titulo}}", this.assunto);
            body = body.Replace("{{nome_destinatario}}", destinatario);
            body = body.Replace("{{conteudo}}", conteudo);

            return body;
        }

        public bool Send()
        {
            try
            {
                foreach (var item in destinatarios)
                {
                    MailMessage message = new MailMessage();
                    message.To.Add(item);

                    message.Subject = assunto;
                    message.IsBodyHtml = corpoHtml;

                    var messageContent = this.GenerateBodyFromTemplate("Padrao", item, corpo);
                    message.Body = messageContent;

                    message.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                    message.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                    message.From = new MailAddress(remetente, remetenteNome);

                    SmtpClient smtp = new SmtpClient(smtpServidor, smtpPorta);

                    smtp.EnableSsl = enableSsl;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("dashboard.notificacoes@engefibra.net.br", "das#455");

                    if (enableSsl)
                    {
                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    }

                    smtp.Send(message);
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}

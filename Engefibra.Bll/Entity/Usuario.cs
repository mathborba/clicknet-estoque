using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class Usuario
    {
        #region .: CRUD :.
        public static List<Data.Models.Usuario> GetAll()
        {
            using (var db = new Data.Context.AppContext())
            {
                return db.Usuario.Include("Pessoa").ToList();
            }
        }

        public static Data.Models.Usuario Get(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                return db.Usuario.Include("Pessoa").Where(c => c.Id == id).FirstOrDefault();
            }
        }

        public static void Alter(Data.Models.Usuario model)
        {
            using (var db = new Data.Context.AppContext())
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static bool Add(Data.Models.Usuario model)
        {
            bool sucesso = true;

            using (var db = new Data.Context.AppContext())
            {
                db.Usuario.Add(model);
                db.SaveChanges();
            }
            return sucesso;
        }

        public static void Delete(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                Data.Models.Usuario model = db.Usuario.Find(id);
                db.Usuario.Remove(model);
                db.SaveChanges();
            }
        }

        #endregion

        /// <summary>
        /// Valida a autenticação de um usuário no sistema
        /// </summary>
        /// <param name="usuario">Usuário.</param>
        /// <param name="senha">Senha.</param>
        /// <returns></returns>
        public static Data.Models.Usuario ValidateLogin(string usuario, string senha)
        {
            using (var db = new Data.Context.AppContext())
            {
                return db.Usuario
                        .Include("Pessoa")
                        .Where(x => x.Login == usuario && x.Senha == senha && x.Ativo == true).FirstOrDefault();
            }
        }

        /// <summary>
        /// Enviar e-mail para a direção da empresa
        /// </summary>
        /// <param name="nome">Remetente</param>
        /// <param name="assunto">Assunto.</param>
        /// <param name="corpo">Corpo da mensagem</param>
        public static void EmailDiretoria(string nome, string assunto, string corpo)
        {
            using (var db = new Data.Context.AppContext())
            {
                var destinatarios = db.Pessoa.Where(x => x.ObraNotificacao == true).Select(x => x.Email).ToList();

                var mailer = new Mailer();

                mailer.assunto = "[Engefibra]["+ nome +"] Fale com a Direção: " + assunto;
                mailer.destinatarios = destinatarios;
                mailer.tipoEnvioEmail = MailerSendType.Outros;
                mailer.corpo = "Enviado por: <br />" + nome + "<br />" + corpo;
                mailer.corpoHtml = true;
                mailer.Send();
            }
        }
    }
}

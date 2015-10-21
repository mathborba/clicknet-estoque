using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class Usuario
    {
        public static List<Data.Models.Usuario> GetAll()
        {
            using (var db = new Data.Context.AppContext())
            {
                return db.Usuario.ToList();
            }
        }

        public static Data.Models.Usuario Get(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                return db.Usuario.Find(id);
            }
        }

        public static Data.Models.Usuario ValidateLogin(string usuario, string senha)
        {
            using (var db = new Data.Context.AppContext())
            {
                return db.Usuario
                        .Include("Pessoa")
                        .Where(x => x.Login == usuario && x.Senha == senha && x.Ativo == true).FirstOrDefault();
            }
        }
    }
}

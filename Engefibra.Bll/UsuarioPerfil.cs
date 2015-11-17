using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class UsuarioPerfil
    {
        public static List<string> GetCommaSeparatedUserRole(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                return db.UsuarioPerfil.Include("Perfil").Include("Usuario").Where(x => x.UsuarioId == id)
                    .Select(c => c.Perfil.Nome).ToList();
            }
        }
    }
}
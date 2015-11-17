using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class VeiculoUtilizacao
    {
        #region .: CRUD :.
        public static Data.Models.VeiculoUtilizacao Get(int id)
        {
            var model = new Data.Models.VeiculoUtilizacao();

            using(var db = new Data.Context.AppContext())
            {
                model = db.VeiculoUtilizacao.Include("Pessoa")
                    .Include("Veiculo")
                    .Include("VeiculoUtilizacaoStatus").Where(e => e.Id == id).FirstOrDefault();
            }
            return model;
        }

        public static List<Data.Models.VeiculoUtilizacao> GetAll(bool orderBy = false)
        {
            var model = new List<Data.Models.VeiculoUtilizacao>();

            using (var db = new Data.Context.AppContext())
            {
                model = db.VeiculoUtilizacao.Include("Pessoa")
                    .Include("Veiculo")
                    .Include("VeiculoUtilizacaoStatus").ToList();

                if (orderBy)
                    model = model.OrderByDescending(c => c.Id).ToList();
            }
            return model;
        }

        public static bool Add(Data.Models.VeiculoUtilizacao model)
        {
            bool sucesso = true;

            using (var db = new Data.Context.AppContext())
            {
                db.VeiculoUtilizacao.Add(model);
                db.SaveChanges();
            }
            return sucesso;
        }

        public static void Delete(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                Data.Models.VeiculoUtilizacao model = db.VeiculoUtilizacao.Find(id);
                db.VeiculoUtilizacao.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
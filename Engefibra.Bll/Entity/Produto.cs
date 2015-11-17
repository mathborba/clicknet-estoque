using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class Produto
    {
        #region .: CRUD :.
        public static Data.Models.Produto Get(int id)
        {
            var model = new Data.Models.Produto();

            using(var db = new Data.Context.AppContext())
            {
                model = db.Produto.Where(e => e.Id == id).FirstOrDefault();
            }
            return model;
        }

        public static List<Data.Models.Produto> GetAll(bool orderBy = false)
        {
            var model = new List<Data.Models.Produto>();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Produto.ToList();
                if (orderBy)
                    model = model.OrderByDescending(c => c.Id).ToList();
            }
            return model;
        }

        public static bool Add(Data.Models.Produto model)
        {
            bool sucesso = true;

            using (var db = new Data.Context.AppContext())
            {
                db.Produto.Add(model);
                db.SaveChanges();
            }
            return sucesso;
        }

        public static void Delete(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                Data.Models.Produto model = db.Produto.Find(id);
                db.Produto.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
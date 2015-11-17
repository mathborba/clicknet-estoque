using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class Estoque
    {
        #region .: CRUD :.
        public static Data.Models.Estoque Get(int id)
        {
            var model = new Data.Models.Estoque();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Estoque.Where(x => x.Id == id).FirstOrDefault();
            }

            return model;
        }

        public static List<Data.Models.Estoque> GetAll(bool orderBy = false)
        {
            var model = new List<Data.Models.Estoque>();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Estoque.ToList();
                if (orderBy)
                    model = model.OrderByDescending(c => c.Id).ToList();
            }

            return model;
        }

        public static bool Add(Data.Models.Estoque model)
        {
            bool sucesso = true;

            using (var db = new Data.Context.AppContext())
            {
                db.Estoque.Add(model);
                db.SaveChanges();
            }
            return sucesso;
        }

        public static void Alter(Data.Models.Estoque model)
        {
            using (var db = new Data.Context.AppContext())
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (var db = new Data.Context.AppContext())
            {
                Data.Models.Estoque model = db.Estoque.Find(id);
                db.Estoque.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
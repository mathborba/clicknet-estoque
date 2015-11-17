using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class Contato
    {
        #region .: CRUD :.
        public static Data.Models.Contato Get(int id)
        {
            var model = new Data.Models.Contato();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Contato.Where(x => x.Id == id).FirstOrDefault();
            }

            return model;
        }

        public static List<Data.Models.Contato> GetAll(bool orderBy = false)
        {
            var model = new List<Data.Models.Contato>();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Contato.ToList();
                if (orderBy)
                    model = model.OrderByDescending(c => c.Id).ToList();
            }

            return model;
        }

        public static bool Add(Data.Models.Contato model)
        {
            bool sucesso = true;

            using (var db = new Data.Context.AppContext())
            {
                db.Contato.Add(model);
                db.SaveChanges();
            }
            return sucesso;
        }

        public static void Alter(Data.Models.Contato model)
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
                Data.Models.Contato model = db.Contato.Find(id);
                db.Contato.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
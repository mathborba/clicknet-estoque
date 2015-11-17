using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class Pessoa
    {
        #region .: CRUD :.
        public static Data.Models.Pessoa Get(int id)
        {
            var model = new Data.Models.Pessoa();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Pessoa.Where(x => x.Id == id).FirstOrDefault();
            }

            return model;
        }

        public static List<Data.Models.Pessoa> GetAll(bool orderBy = false)
        {
            var model = new List<Data.Models.Pessoa>();

            using (var db = new Data.Context.AppContext())
            {
                model = db.Pessoa.ToList();
                if (orderBy)
                    model = model.OrderByDescending(c => c.Id).ToList();
            }

            return model;
        }

        public static bool Add(Data.Models.Pessoa model)
        {
            bool sucesso = true;

            using (var db = new Data.Context.AppContext())
            {
                db.Pessoa.Add(model);
                db.SaveChanges();
            }
            return sucesso;
        }

        public static void Alter(Data.Models.Pessoa model)
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
                Data.Models.Pessoa model = db.Pessoa.Find(id);
                db.Pessoa.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepo<T> : IRepository<T> where T : BaseEntity
    {

        internal DataContext context;
        internal DbSet<T> dbSet;

        public SQLRepo(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var item = dbSet.Find(Id);
            if (context.Entry(item).State == EntityState.Detached)
                dbSet.Attach(item);

            dbSet.Remove(item);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public IQueryable<T> ItemsColletion()
        {
            return dbSet;
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified; // need to be with update, when we call save changes look to this method ? 

        }
    }
}

using MyShop.Core.Models;
using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        private ObjectCache cache = MemoryCache.Default;
        private List<T> itemsList;
        private string className;
        
        public InMemoryRepository()
        {
            className = typeof(T).Name.ToString();
            itemsList = cache[className] as List<T>;
            if (itemsList == null)
                itemsList = new List<T>();

        }

        public void Commit()
        {
            cache[className] = itemsList;
        }

        public void Insert(T t)
        {
            itemsList.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = itemsList.Find(i => i.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + "Not found");
            }
        }

        public T Find(string Id)
        {
            T tToFind = itemsList.Find(i => i.Id == Id);
            if (tToFind != null)
                return tToFind;
            else
                throw new Exception("Id: " + Id + " Not found");
        }

        public IQueryable<T> ItemsColletion()
        {
            return itemsList.AsQueryable();
        }

        public void Delete(string Id)
        {
            T itemToRemove = itemsList.Find(i => i.Id == Id);
            if (!itemToRemove.Equals(null))
            {
                itemsList.Remove(itemToRemove);
            }
            else
                throw new Exception("Id: " + Id + " Not found");
            
        }

    }

}

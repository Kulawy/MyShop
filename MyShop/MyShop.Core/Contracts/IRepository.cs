using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        IQueryable<T> ItemsColletion();
        void Update(T t);
    }
}
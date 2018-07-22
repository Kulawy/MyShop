using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> productsList;

        public ProductRepository()
        {
            productsList = cache["products"] as List<Product>;
            if ( productsList == null)
            {
                productsList = new List<Product>();
            }
        }

        //save product list to cache memory 
        public void Commit()
        {
            cache["products"] = productsList;
        }

        public void Insert(Product p)
        {
            productsList.Add(p);
        }

        public void Update(Product prod)
        {
            Product productToUpdate = productsList.Find(p => p.Id == prod.Id);

            if ( productToUpdate != null)
            {
                productToUpdate = prod;

            }
            else
            {
                throw new Exception("Product not found");

            }
        }

        public Product Find(string id)
        {
            Product product = productsList.Find(p => p.Id == id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> ProductCollection()
        {
            return productsList.AsQueryable();
        }

        public void Delete(string id)
        {
            Product productToDelete = productsList.Find(p => p.Id == id);

            if (productToDelete != null)
            {
                productsList.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

    }
}

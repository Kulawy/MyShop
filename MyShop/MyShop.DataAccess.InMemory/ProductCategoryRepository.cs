using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategoriesList;

        public ProductCategoryRepository()
        {
            productCategoriesList = cache["productCategories"] as List<ProductCategory>;
            if (productCategoriesList == null)
            {
                productCategoriesList = new List<ProductCategory>();
            }
        }

        //save product list to cache memory 
        public void Commit()
        {
            cache["productCategories"] = productCategoriesList;
        }

        public void Insert(ProductCategory pc)
        {
            productCategoriesList.Add(pc);
        }

        public void Update(ProductCategory prodCategory)
        {
            ProductCategory productCategoryToUpdate = productCategoriesList.Find(p => p.Id == prodCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = prodCategory;

            }
            else
            {
                throw new Exception("Product Category not found");

            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory productCategory = productCategoriesList.Find(p => p.Id == id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public IQueryable<ProductCategory> ProductCategoryCollection()
        {
            return productCategoriesList.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory productToDelete = productCategoriesList.Find(p => p.Id == id);

            if (productToDelete != null)
            {
                productCategoriesList.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
    }
}

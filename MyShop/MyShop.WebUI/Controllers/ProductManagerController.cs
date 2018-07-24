using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //declate context as ProductRepository whitch is the list and a service for that list for Products
        private IRepository<Product> context;
        private IRepository<ProductCategory> productCategoriesContext;

        
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategoriesContext = productCategoryContext;

        }
        // GET: ProductManager
        public ActionResult Index()
        {

            List<Product> productsList = context.ItemsColletion().ToList();
            return View(productsList);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel productViewModel = new ProductManagerViewModel(); 
            productViewModel.Product = new Product();
            productViewModel.ProductCategoriesEnumerable = productCategoriesContext.ItemsColletion();
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
            
        }

        
        public ActionResult Edit(string Id)
        {
            Product prod = context.Find(Id);
            if (prod == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel productViewModel = new ProductManagerViewModel();
                productViewModel.Product = prod;
                productViewModel.ProductCategoriesEnumerable = productCategoriesContext.ItemsColletion();
                return View(productViewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if(!ModelState.IsValid)
                    return View(product);
                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }


    }
}
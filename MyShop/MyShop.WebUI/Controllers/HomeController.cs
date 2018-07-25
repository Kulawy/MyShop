using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        private IRepository<Product> context;
        private IRepository<ProductCategory> productCategoriesContext;


        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategoriesContext = productCategoryContext;

        }
        public ActionResult Index(string Category = null)
        {
            //List<Product> products = context.ItemsColletion().ToList();
            List<Product> products;
            List<ProductCategory> categories = productCategoriesContext.ItemsColletion().ToList();

            if ( Category == null)
                products = context.ItemsColletion().ToList();
            else
                products = context.ItemsColletion().Where(p => p.Category == Category).ToList();

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = categories;

            return View(model);
        }

        public ActionResult Detail(string Id)
        {
            Product prod = context.Find(Id);
            if (prod != null)
                return View(prod);
            else
                return HttpNotFound();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
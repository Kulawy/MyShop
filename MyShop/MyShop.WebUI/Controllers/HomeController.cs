using MyShop.Core.Contracts;
using MyShop.Core.Models;
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
        public ActionResult Index()
        {
            List<Product> products = context.ItemsColletion().ToList();
            return View(products);
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
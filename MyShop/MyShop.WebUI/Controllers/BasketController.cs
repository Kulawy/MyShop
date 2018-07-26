using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketServiceContext;

        public BasketController(IBasketService basketServiceContextInput)
        {
            basketServiceContext = basketServiceContextInput;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketServiceContext.GetBasketItems(this.HttpContext);
            return View();
        }

        public ActionResult AddToBasket(string Id)
        {
            basketServiceContext.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketServiceContext.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketServiceContext.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }


    }
}
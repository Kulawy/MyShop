using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BASKET_SESSION_NAME = " eCommerceBasket";

        public BasketService( IRepository<Product> ProductContextInput, IRepository<Basket> BasketContextInput)
        {
            productContext = ProductContextInput;
            basketContext = BasketContextInput;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BASKET_SESSION_NAME);
            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                    basket = basketContext.Find(basketId);
                else
                {
                    if (createIfNull)
                        basket = CreateNewBasket(httpContext);
                }
            }
            else
            {
                if (createIfNull)
                    basket = CreateNewBasket(httpContext);
            }             
            return  basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContextInput)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BASKET_SESSION_NAME);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContextInput.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContextInput, string productIdInput)
        {
            Basket basket = GetBasket(httpContextInput, true);
            BasketItem bItem = basket.BasketItems.FirstOrDefault(i => i.ProductId == productIdInput);
            if ( bItem == null)
            {
                bItem = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productIdInput,
                    Quantity = 1
                };
                basket.BasketItems.Add(bItem);
            }
            else
            {
                bItem.Quantity++;
            }
            basketContext.Commit();
        }

        
        public void RemoveFromBasket(HttpContextBase httpContextInput, string itemIdInput)
        {
            Basket basket = GetBasket(httpContextInput, true);
            BasketItem bItem= basket.BasketItems.FirstOrDefault(i => i.Id == itemIdInput);

            if (bItem != null)
            {
                basket.BasketItems.Remove(bItem);
                basketContext.Commit();
            }

        }





    }

}

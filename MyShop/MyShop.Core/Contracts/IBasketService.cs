using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContextInput, string productIdInput);
        void RemoveFromBasket(HttpContextBase httpContextInput, string itemIdInput);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContextInput);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContextInput);

    }
}

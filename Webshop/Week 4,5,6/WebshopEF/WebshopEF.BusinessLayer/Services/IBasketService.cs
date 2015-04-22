using System;
namespace WebshopEF.BusinessLayer.Services
{
    public interface IBasketService
    {
        void AddItemToBasket(WebshopEF.Models.Basket b);
        System.Collections.Generic.List<WebshopEF.Models.Basket> GetBasketItems(string user);
        void UpdateBasket(string user);
    }
}

using System;
using WebshopEF.Models;
namespace WebshopEF.Repositories
{
    interface IBasketRepository:IGenericRepository<Basket>
    {
        void AddItemToBasket(WebshopEF.Models.Basket b);
        System.Collections.Generic.List<WebshopEF.Models.Basket> GetBasketItems(string user);
        void UpdateBasket(string user);
    }
}

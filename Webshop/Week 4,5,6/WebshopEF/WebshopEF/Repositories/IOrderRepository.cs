using System;
namespace WebshopEF.Repositories
{
    interface IOrderRepository
    {
        void SaveOrder(WebshopEF.Models.Order order, System.Collections.Generic.List<WebshopEF.Models.Basket> items);
    }
}

using System;
namespace WebshopEF.BusinessLayer.Services
{
    public interface IOrderService
    {
        void SaveOrder(WebshopEF.Models.Order order);
        void SaveOrderToQueue(string order);
    }
}

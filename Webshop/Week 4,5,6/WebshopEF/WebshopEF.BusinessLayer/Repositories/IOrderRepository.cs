using System;
using WebshopEF.Models;
namespace WebshopEF.Repositories
{
    public interface IOrderRepository
    {
        void SaveOrder(Order order);
        void SaveOrderToQueue(string order);
    }
}

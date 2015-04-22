using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopEF.Models;
using WebshopEF.Repositories;

namespace WebshopEF.BusinessLayer.Services
{
    public class OrderService : WebshopEF.BusinessLayer.Services.IOrderService
    {
        private IOrderRepository repoOrder;

        public OrderService(IOrderRepository repoOrder)
        {
            this.repoOrder = repoOrder;
        }

        public void SaveOrder(Order order)
        {
            repoOrder.SaveOrder(order);
        }

        public void SaveOrderToQueue(string order)
        {
            repoOrder.SaveOrderToQueue(order);
        }
    }
}

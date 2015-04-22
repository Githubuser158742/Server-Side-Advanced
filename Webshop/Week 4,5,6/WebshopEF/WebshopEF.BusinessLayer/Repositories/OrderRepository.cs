using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopEF.Models;
using System.Data.Entity;
using WebshopEF.BusinessLayer.Context;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WebshopEF.Repositories
{
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
        public void SaveOrder(Order order)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                foreach (OrderLine ol in order.Items)
                {
                    context.Entry<Device>(ol.Basket.Device).State = EntityState.Unchanged;
                    context.Entry<Basket>(ol.Basket).State = EntityState.Unchanged;
                }

                context.Order.Add(order);
                context.SaveChanges();
            }
        }

        public void SaveOrderToQueue(string order)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("orderinfo");
            queue.CreateIfNotExists();
            CloudQueueMessage message = new CloudQueueMessage(order);
            queue.AddMessage(message);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using WebshopEF.Repositories;
using WebshopEF.BusinessLayer.Services;
using Newtonsoft.Json;
using WebshopEF.Models;

namespace OrderWebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            OrderRepository or = new OrderRepository();
            OrderService os = new OrderService(or);

            Order o = JsonConvert.DeserializeObject<Order>(message);
            os.SaveOrder(o);
        }
    }
}

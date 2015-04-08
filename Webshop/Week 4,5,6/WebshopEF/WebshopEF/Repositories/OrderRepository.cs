using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopEF.Models;
using System.Data.Entity;

namespace WebshopEF.Repositories
{
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
        public void SaveOrder(Order order,List<Basket>items)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                List<OrderLine> orderlines = new List<OrderLine>();

                foreach(Basket b in items)
                {
                    OrderLine ol = new OrderLine();
                    ol.Basket = b;
                    orderlines.Add(ol);
                }
                Order o = new Order();
                o.Naam = order.Naam;
                o.Voornaam = order.Voornaam;
                o.Postcode = order.Postcode;
                o.Zipcode = order.Zipcode;
                o.Adres = order.Adres;
                o.Items = orderlines;

                foreach(OrderLine ol in o.Items)
                {
                    context.Entry<Device>(ol.Basket.Device).State = EntityState.Unchanged;
                    context.Entry<Basket>(ol.Basket).State = EntityState.Unchanged;
                    context.Entry<OrderLine>(ol).State = EntityState.Added;
                }

                context.Order.Add(o);
                context.SaveChanges();
            }
        }
    }
}
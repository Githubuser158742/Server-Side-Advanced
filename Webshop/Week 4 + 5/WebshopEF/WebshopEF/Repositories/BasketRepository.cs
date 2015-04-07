using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopEF.Models;
using System.Data.Entity;

namespace WebshopEF.Repositories
{
    public class BasketRepository:GenericRepository<Basket>, IBasketRepository
    {
        public void AddItemToBasket(Basket b)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                Basket newBasket = new Basket();
                newBasket = b;

                context.Entry<Device>(newBasket.Device).State = EntityState.Unchanged;
                context.Entry(newBasket).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public List<Basket> GetBasketItems(string user)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                List<Basket> baskets = new List<Basket>();
                var query = (from b in context.Basket.Include(d => d.Device)
                             where b.User == user && b.IsOrdered == false
                             select b);

                return query.ToList<Basket>();
            }
        }

        public void UpdateBasket(string user)
        {
            List<Basket> baskets = GetBasketItems(user);

            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                foreach(Basket b in baskets)
                {
                    b.IsOrdered = true;
                    context.Entry<Basket>(b).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }
    }
}
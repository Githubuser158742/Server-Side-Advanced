using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopEF.Models;
using WebshopEF.Repositories;

namespace WebshopEF.BusinessLayer.Services
{
    public class BasketService : WebshopEF.BusinessLayer.Services.IBasketService
    {
        private IBasketRepository repoBasket = null;

        public BasketService(IBasketRepository repoBasket)
        {
            this.repoBasket = repoBasket;
        }

        public void AddItemToBasket(Basket b)
        {
            repoBasket.AddItemToBasket(b);
        }

        public List<Basket> GetBasketItems(string user)
        {
            return repoBasket.GetBasketItems(user);
        }

        public void UpdateBasket(string user)
        {
            repoBasket.UpdateBasket(user);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopEF.Models;

namespace WebshopEF.Helper
{
    public class PriceCalculator
    {
        public List<Basket> BasketItems { get; set; }
        
        public PriceCalculator(List<Basket> items)
        {
            this.BasketItems = items;
        }

        public double BerekenPrijs()
        {
            double totalePrijs = 0;
            foreach (Basket b in BasketItems)
                totalePrijs += b.Device.Price * b.Aantal;

            return totalePrijs;
        }
    }
}
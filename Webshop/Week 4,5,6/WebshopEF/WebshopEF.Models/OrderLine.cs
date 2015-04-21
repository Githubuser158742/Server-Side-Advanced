using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopEF.Models
{
    public class OrderLine
    {
        public int ID { get; set; }
        public Basket Basket { get; set; }
    }
}
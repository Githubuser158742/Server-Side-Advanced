using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopEF.Models
{
    public class Basket
    {
        public int ID { get; set; }
        public string User { get; set; }
        public DateTime Timestamp { get; set; }
        public int Aantal { get; set; }
        public Device Device { get; set; }
        public bool IsOrdered { get; set; }
    }
}
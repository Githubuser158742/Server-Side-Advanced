using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopEF.Models
{
    public class Device
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double RentPrice { get; set; }
        public int Stock { get; set; }
        public string Picture { get; set; }
        public List<OS> OperatingSystems { get; set; }
        public List<Framework> Frameworks { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebshopEF.Models
{
    public class Framework
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Device> Devices { get; set; }
    }
}
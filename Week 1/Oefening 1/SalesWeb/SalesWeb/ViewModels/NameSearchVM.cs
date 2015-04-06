using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesWeb.ViewModels
{
    public class NameSearchVM
    {
        [Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
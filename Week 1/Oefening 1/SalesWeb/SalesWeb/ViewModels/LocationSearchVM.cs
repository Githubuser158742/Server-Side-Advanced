using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SalesWeb.ViewModels
{
    public class LocationSearchVM
    {
        [Required]
        //[Range(typeof(String),"A", "E")] //jQ NOK
        [RegularExpression(@"[A-E]",ErrorMessage="Must be between A and E")]
        public string Row { get; set; }

        [Required]
        [Range(1, 20)]
        public int Position { get; set; }


        public List<Inventory> Inventories { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.ViewModels {
    public class SearchVM {
        public NameSearchVM NameSearch { get; set; }

        public LocationSearchVM LocationSearch { get; set; }
    }
}
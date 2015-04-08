using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopEF.Models;

namespace WebshopEF.PresentationModels
{
    public class CreatePM
    {
        public SelectList OperatingSystems { get; set; }
        public SelectList Frameworks { get; set; }
        public Device Device { get; set; }
        public int OperatingSystemId { get; set; }
        public int FrameworkId { get; set; }
        public SelectList SelectedOperatingSystems { get; set; }
        public SelectList SelectedFrameworks { get; set; }
        public string idsOS { get; set; }
        public string idsFR { get; set; }
        public string submit { get; set; }
        public HttpPostedFileBase Upload { get; set; }
        public string Error { get; set; }
    }
}
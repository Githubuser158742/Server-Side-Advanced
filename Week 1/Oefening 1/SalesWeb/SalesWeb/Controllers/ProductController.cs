using SalesWeb.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesWeb.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View(ProductRepository.GetProducts());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            var product = ProductRepository.FindById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

    }
}

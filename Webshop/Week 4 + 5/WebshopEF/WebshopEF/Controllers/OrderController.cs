using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopEF.Models;
using WebshopEF.Services;

namespace WebshopEF.Controllers
{
    public class OrderController : Controller
    {
        private IProductService ps;
        public OrderController(IProductService ps)
        {
            this.ps = ps;
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveOrder(Order o)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Checkout");

            List<Basket> items = ps.GetBasketItems(User.Identity.Name);

            ps.SaveOrder(o, items);
            ps.UpdateBasket(User.Identity.Name);

            return RedirectToAction("Thanks");
        }

        [HttpGet]
        public ViewResult Thanks()
        {
            return View();
        }
    }
}
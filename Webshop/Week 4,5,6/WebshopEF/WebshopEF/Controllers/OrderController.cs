using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopEF.Models;
using WebshopEF.BusinessLayer.Services;

namespace WebshopEF.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService os = null;
        private IBasketService bs = null;
        public OrderController(IOrderService os,IBasketService bs)
        {
            this.os = os;
            this.bs = bs;
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

            List<Basket> items = bs.GetBasketItems(User.Identity.Name);
            List<OrderLine> orderlines = new List<OrderLine>();

            foreach(Basket b in items)
            {
                OrderLine ol = new OrderLine();
                ol.Basket = b;
                orderlines.Add(ol);
            }

            o.Items = orderlines;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(o);
            os.SaveOrderToQueue(json);
            bs.UpdateBasket(User.Identity.Name);

            return RedirectToAction("Thanks");
        }

        [HttpGet]
        public ViewResult Thanks()
        {
            return View();
        }
    }
}
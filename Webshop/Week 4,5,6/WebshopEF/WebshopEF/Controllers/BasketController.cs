using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopEF.Helper;
using WebshopEF.Models;
using WebshopEF.Services;

namespace WebshopEF.Controllers
{
    public class BasketController : Controller
    {
        private IProductService ps;

        public BasketController(IProductService ps)
        {
            this.ps = ps;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Add(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            Device d = new Device();
            d = ps.GetDeviceById(id.Value);

            return View(d);
        }

        [HttpPost]
        public ActionResult Add(int? id, int? number)
        {
            if (!id.HasValue || !number.HasValue || number.Value == 0)
                return RedirectToAction("Index", "Device");

            Basket b = new Basket();
            b.Aantal = number.Value;
            b.Timestamp = DateTime.Now;
            b.User = User.Identity.Name;
            b.Device = ps.GetDeviceById(id.Value);
            ps.AddItemToBasket(b);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            List<Basket> items = new List<Basket>();

            string user = User.Identity.Name;
            items = ps.GetBasketItems(user);

            PriceCalculator calculator = new PriceCalculator(items);
            ViewBag.TotalePrijs = calculator.BerekenPrijs();

            return View(items);
        }

        public int ItemsInBasket()
        {
            return ps.GetBasketItems(User.Identity.Name).Count;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopEF.Helper;
using WebshopEF.Models;
using WebshopEF.BusinessLayer.Services;

namespace WebshopEF.Controllers
{
    public class BasketController : Controller
    {
        private IDeviceService ds = null;
        private IBasketService bs = null;

        public BasketController(IDeviceService ds,IBasketService bs)
        {
            this.ds = ds;
            this.bs = bs;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            string user = User.Identity.Name;
            List<Basket>items = bs.GetBasketItems(user);

            PriceCalculator calculator = new PriceCalculator(items);
            ViewBag.TotalePrijs = calculator.BerekenPrijs();

            return View(items);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Add(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            Device d = new Device();
            d = ds.GetDeviceById(id.Value);

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
            b.Device = ds.GetDeviceById(id.Value);
            bs.AddItemToBasket(b);

            return RedirectToAction("Index");
        }

        

        public int ItemsInBasket()
        {
            return bs.GetBasketItems(User.Identity.Name).Count;
        }
    }
}
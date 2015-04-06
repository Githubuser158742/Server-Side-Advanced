using SalesWeb;
using SalesWeb.Models;
using SalesWeb.Models.DAL;
using SalesWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesInventory.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Search()
        {
            SearchVM model = new SearchVM
            {
                NameSearch = new NameSearchVM(),
                LocationSearch = new LocationSearchVM()
            };
            return View(model);
        }



        //GET
        public ActionResult NameSearch(NameSearchVM model)
        {

            model.Products = ProductRepository.FindByName(model.Name);

            if (ModelState.IsValid)
            {
                if (model.Products == null)
                {
                    return HttpNotFound();
                }

                if (model.Products.Count == 0)
                {
                    ModelState.AddModelError("NoProductData", "No products found .");
                    return View("Search", new SearchVM() { NameSearch = model, LocationSearch = new LocationSearchVM() });
                }


                foreach (Product product in model.Products)
                {
                    product.Inventories = InventoryRepository.FindInventoriesByProductId(product.ProductID);
                }

                return View("~/Views/Search/NameSearchIndex.cshtml", model);
            }
            else
            {
                if (model.Name == null) {
                  return RedirectToAction("Index", "Product");
                } 
                
                return View("Search", new SearchVM() { NameSearch = model, LocationSearch = new LocationSearchVM() });
            }
        }

        
        //GET
        public ActionResult LocationSearch(LocationSearchVM model)
        {
            if (ModelState.IsValid)
            {

               model.Inventories = InventoryRepository.FindInventoriesByLocation(model.Row, model.Position);
               if (model.Inventories == null)
                {
                    return HttpNotFound();
                }

               if (model.Inventories.Count == 0)
               {

                    ModelState.AddModelError("NoLocationData", "No data found for this location.");
                    return View("Search", new SearchVM() { NameSearch = new NameSearchVM(), LocationSearch = model });
                
                }

                return View("~/Views/Inventory/Index.cshtml", model.Inventories);
            }
            else
            {
                return View("Search", new SearchVM() { NameSearch = new NameSearchVM(), LocationSearch = model });
            }
        }



    }
}

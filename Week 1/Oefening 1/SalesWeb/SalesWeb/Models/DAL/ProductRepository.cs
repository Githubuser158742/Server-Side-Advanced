using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data.Entity;

namespace SalesWeb.Models.DAL
{
    public class ProductRepository
    {

        //methoden
        public static List<Product> GetProducts()
        {
            ////0.vars          
            ////1. SQL instructie 
            //string sSQL = "SELECT * FROM [Products]";

            ////2.reading uitvoeren
            //return GetList(sSQL);
            using(SalesDBEntities context = new SalesDBEntities())
            {
                var query = (from p in context.Products
                             select p);
                return query.ToList<Product>();
            }
        }


        public static Product FindById(int productID)
        {
            ////0.vars          
            ////1. SQL instructie 
            //string sSQL = "SELECT * FROM [Products]";
            //sSQL += " WHERE [Products].[ProductID] = @productID";

            ////2. SQL parameters
            //DbParameter idPar = Database.AddParameter("@productID", productID);

            ////3. Haal data op en controleer op null/lege velden
            //return GetList(sSQL, idPar)[0];
            using(SalesDBEntities context = new SalesDBEntities())
            {
                var query = (from p in context.Products
                             where p.ProductID == productID
                             select p);
                return query.Single<Product>();
            }
        }


        public static List<Product> FindByName(string productSearch)
        {
            ////0.vars                   
            ////1. SQL instructie 
            //string sSQL = "SELECT * FROM [Products]";
            //sSQL += " WHERE [Products].[Name] Like @productSearch";
            //sSQL += " ORDER BY NAME ASC";

            ////2. SQL parameters
            //DbParameter searchPar = DBHelper.Database.AddParameter("@productSearch", "%" + productSearch + "%");

            ////3. Haal data op en controleer op null/lege velden
            //return GetList(sSQL, searchPar);
            using(SalesDBEntities context = new SalesDBEntities())
            {
                var query = (from p in context.Products.Include(t => t.Inventories)
                             where p.Name.Contains(productSearch)
                             select p);
                return query.ToList<Product>();
            }
        }
    }
}
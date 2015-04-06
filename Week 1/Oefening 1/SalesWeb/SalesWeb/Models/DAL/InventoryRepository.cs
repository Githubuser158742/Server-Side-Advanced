using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SalesWeb.Models.DAL
{
    public class InventoryRepository
    {

        //methoden
        public static List<Inventory> GetInventories()
        {
            ////1. SQL instructie 
            //string sSQL = "SELECT * FROM [Inventory]";

            ////2.reading uitvoeren
            //return GetList(sSQL);
            using (SalesDBEntities context = new SalesDBEntities())
            {
                var query = (from i in context.Inventories
                             select i);
                return query.ToList<Inventory>();
            }
        }

        public static List<Inventory> FindInventoriesByProductId(int productID)
        {
            ////1. SQL instructie 
            //string sSQL = "SELECT * FROM [Inventory]";
            //sSQL += " WHERE [Inventory].[ProductID] = @productID";

            ////2. SQL parameters
            //DbParameter idPar = Database.AddParameter("@productID", productID);

            ////3. Haal data op en controleer op null/lege velden
            //return GetList(sSQL, idPar);
            //}
            using (SalesDBEntities context = new SalesDBEntities())
            {
                var query = (from i in context.Inventories
                             where i.ProductID == productID
                             select i);
                return query.ToList<Inventory>();
            }
        }

        public static List<Inventory> FindInventoriesByLocation(string row, int position)
        {
            ////1. SQL instructie 
            //string sSQL = "SELECT * FROM [Inventory]";
            //sSQL += " WHERE [Inventory].[Row] = @Row";
            //sSQL += " AND [Inventory].[Position] = @Position";

            ////2. SQL parameters
            //DbParameter rowPar = Database.AddParameter("@Row", row);
            //DbParameter positionPar = Database.AddParameter("@Position", position);

            ////3. Haal data op en controleer op null/lege velden
            //return GetList(sSQL, rowPar, positionPar);
            using(SalesDBEntities context = new SalesDBEntities())
            {
                var query = (from i in context.Inventories.Include(i => i.Product)
                             where i.Row == row && i.Position == position
                             select i);
                return query.ToList<Inventory>();
            }
        }      
    }
}
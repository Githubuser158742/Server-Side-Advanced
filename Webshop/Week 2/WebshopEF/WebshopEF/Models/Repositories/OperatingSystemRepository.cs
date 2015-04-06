using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopEF.Models;

namespace WebshopEF.Repositories
{
    public class OperatingSystemRepository
    {
        public List<OS> GetOperatingSystems()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from o in context.OS select o);
                return query.ToList<OS>();
            }
        }

        public OS GetOperatingSystemById(int id)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from o in context.OS
                             where o.ID == id
                             select o);
                return query.SingleOrDefault<OS>();
            }
        }
    }
}
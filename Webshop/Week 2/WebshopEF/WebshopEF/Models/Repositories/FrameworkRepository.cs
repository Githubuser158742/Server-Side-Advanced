using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopEF.Models;

namespace WebshopEF.Repositories
{
    public class FrameworkRepository
    {
        public List<Framework> GetFrameworks()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from f in context.Framework
                             select f);
                return query.ToList<Framework>();
            }
        }

        public Framework GetFrameworkById(int id)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from f in context.Framework
                             where f.ID == id
                             select f);
                return query.SingleOrDefault<Framework>();
            }            
        }
    }
}
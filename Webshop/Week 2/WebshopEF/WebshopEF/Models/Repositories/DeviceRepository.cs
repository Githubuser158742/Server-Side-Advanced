using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopEF.Models;
using System.Data.Entity;

namespace WebshopEF.Repositories
{
    public class DeviceRepository
    {
        public List<Device> GetDevices()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from d in context.Device select d);
                return query.ToList<Device>();
            }
        }

        public Device GetDeviceById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var query = (from d in context.Device
                             where d.ID == id
                             select d);
                return query.SingleOrDefault<Device>();
            }
        }

        public void CreateDevice(Device d)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                Device newDevice = new Device();
                newDevice = d;

                foreach (OS o in d.OperatingSystems)
                    context.Entry<OS>(o).State = EntityState.Unchanged;

                foreach (Framework f in d.Frameworks)
                    context.Entry<Framework>(f).State = EntityState.Unchanged;

                context.Device.Add(d);
                context.SaveChanges();
            }
        }
    }
}
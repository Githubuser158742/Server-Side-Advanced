using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebshopEF.Models;
using System.Data.Entity;
using WebshopEF.BusinessLayer.Context;

namespace WebshopEF.Repositories
{
    public class DeviceRepository: GenericRepository<Device>, WebshopEF.Repositories.IDeviceRepository
    {
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
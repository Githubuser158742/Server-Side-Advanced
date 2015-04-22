using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebshopEF.Models;
using WebshopEF.Repositories;

namespace WebshopEF.BusinessLayer.Services
{
    public class DeviceService : WebshopEF.BusinessLayer.Services.IDeviceService
    {
        private IGenericRepository<OS> repoOS = null;
        private IGenericRepository<Framework> repoFramework = null;
        private IDeviceRepository repoDevice = null;

        public DeviceService(IGenericRepository<OS> repoOS, IGenericRepository<Framework> repoFramework,IDeviceRepository repoDevice)
        {
            this.repoOS = repoOS;
            this.repoFramework = repoFramework;
            this.repoDevice = repoDevice;
        }

        public List<OS> GetOperatingSystems()
        {
            return repoOS.All().ToList();
        }

        public List<Framework> GetFrameworks()
        {
            return repoFramework.All().ToList();
        }

        public List<Device> GetDevices()
        {
            return repoDevice.All().ToList();
        }

        public Device GetDeviceById(int id)
        {
            return repoDevice.GetByID(id);
        }

        public OS GetOperatingSystemById(int id)
        {
            return repoOS.GetByID(id);
        }

        public Framework GetFrameworkById(int id)
        {
            return repoFramework.GetByID(id);
        }

        public void CreateDevice(Device d)
        {
            repoDevice.CreateDevice(d);
        }
         
        public void SaveImage(HttpPostedFileBase p)
        {
            repoDevice.SaveImage(p);
        }
    }
}

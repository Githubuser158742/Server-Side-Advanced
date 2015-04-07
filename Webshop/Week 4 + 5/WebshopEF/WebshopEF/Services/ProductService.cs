using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebshopEF.Models;
using WebshopEF.PresentationModels;
using WebshopEF.Repositories;

namespace WebshopEF.Services
{
    public class ProductService : WebshopEF.Services.IProductService
    {
        private IGenericRepository<OS> repoOS = null;
        private IGenericRepository<Framework> repoFramework = null;
        private IDeviceRepository repoDevice = null;

        public ProductService(IGenericRepository<OS> repoOS,IGenericRepository<Framework> repoFramework, IDeviceRepository repoDevice)
        {
            this.repoOS = repoOS;
            this.repoFramework = repoFramework;
            this.repoDevice = repoDevice;
        }

        public void SaveImage(HttpPostedFileBase p)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //create the blob client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            //retrieve reference to a previously created container
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            //retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(p.FileName);

            //create or overwrite the "myblob" blob with contents from a local file
            blockBlob.UploadFromStream(p.InputStream);
        }

        public List<Device> GetDevices()
        {
            return repoDevice.All().ToList();
        }

        public Device GetDeviceById(int? id)
        {
            return repoDevice.GetByID(id);
        }

        public List<OS> GetOperatingSystems()
        {
            return repoOS.All().ToList();
        }

        public OS GetOperatingSystemById(int id)
        {
            return repoOS.GetByID(id);
        }

        public List<Framework> GetFrameworks()
        {
            return repoFramework.All().ToList();
        }

        public Framework GetFrameworkById(int id)
        {
            return repoFramework.GetByID(id);
        }

        public void CreateDevice(Device d)
        {
            repoDevice.CreateDevice(d);
        }

        public bool ValidateForm(CreatePM pm)
        {
            if (pm.Device.Price < 1) return false;
            if (pm.Device.RentPrice < 1) return false;
            if (pm.Device.Stock < 1) return false;
            if (pm.SelectedFrameworks.Count() < 1) return false;
            if (pm.SelectedOperatingSystems.Count() < 1) return false;
            return true;
        }
        public List<OS> CreateSelectedOS(string ids)
        {
            List<OS> list = new List<OS>();

            if (ids != null)
            {
                string[] items = ids.Split(';');

                foreach (string item in items)
                {
                    if (item != "")
                    {
                        list.Add(GetOperatingSystemById(Convert.ToInt32(item)));
                    }
                }
            }

            return list;
        }
        public List<Framework> CreateSelectedFR(string ids)
        {
            List<Framework> list = new List<Framework>();

            if (ids != null)
            {
                string[] items = ids.Split(';');

                foreach (string item in items)
                {
                    if (item != "")
                    {
                        list.Add(GetFrameworkById(Convert.ToInt32(item)));
                    }
                }
            }
            return list;
        }

        public void AddItemToBasket(Basket b)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Basket newBasket = new Basket();
                newBasket = b;

                context.Entry<Device>(newBasket.Device).State = EntityState.Unchanged;
                context.Entry(newBasket).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public List<Basket> GetBasketItems(string user)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                List<Basket> baskets = new List<Basket>();

                var query = (from b in context.Basket.Include(d => d.Device)
                             where b.User == user && b.IsOrdered == false
                             select b);

                return query.ToList<Basket>();
            }
        }
    }
}
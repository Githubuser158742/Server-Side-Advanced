using System;
using System.Web;
using WebshopEF.Models;
namespace WebshopEF.Repositories
{
    public interface IDeviceRepository:IGenericRepository<Device>
    {
        void CreateDevice(WebshopEF.Models.Device d);
        void SaveImage(HttpPostedFileBase p);
    }
}

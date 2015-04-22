using System;
using System.Web;
namespace WebshopEF.BusinessLayer.Services
{
    public interface IDeviceService
    {
        void CreateDevice(WebshopEF.Models.Device d);
        WebshopEF.Models.Device GetDeviceById(int id);
        System.Collections.Generic.List<WebshopEF.Models.Device> GetDevices();
        WebshopEF.Models.Framework GetFrameworkById(int id);
        System.Collections.Generic.List<WebshopEF.Models.Framework> GetFrameworks();
        WebshopEF.Models.OS GetOperatingSystemById(int id);
        System.Collections.Generic.List<WebshopEF.Models.OS> GetOperatingSystems();
        void SaveImage(HttpPostedFileBase p);
    }
}

using System;
namespace WebshopEF.Services
{
    public interface IProductService
    {
        void CreateDevice(WebshopEF.Models.Device d);
        System.Collections.Generic.List<WebshopEF.Models.Framework> CreateSelectedFR(string ids);
        System.Collections.Generic.List<WebshopEF.Models.OS> CreateSelectedOS(string ids);
        WebshopEF.Models.Device GetDeviceById(int? id);
        System.Collections.Generic.List<WebshopEF.Models.Device> GetDevices();
        WebshopEF.Models.Framework GetFrameworkById(int id);
        System.Collections.Generic.List<WebshopEF.Models.Framework> GetFrameworks();
        WebshopEF.Models.OS GetOperatingSystemById(int id);
        System.Collections.Generic.List<WebshopEF.Models.OS> GetOperatingSystems();
        bool ValidateForm(WebshopEF.PresentationModels.CreatePM pm);
    }
}

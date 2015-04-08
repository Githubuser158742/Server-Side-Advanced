using System;
using WebshopEF.Models;
namespace WebshopEF.Repositories
{
    public interface IDeviceRepository:IGenericRepository<Device>
    {
        void CreateDevice(WebshopEF.Models.Device d);
    }
}

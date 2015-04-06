using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using WebshopEF.Repositories;
using WebshopEF.Models;
using WebshopEF.Services;

namespace WebshopEF
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IGenericRepository<Framework>, GenericRepository<Framework>>();
            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS>>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<IProductService, ProductService>();

            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using WebshopEF.Repositories;
using WebshopEF.Models;
using WebshopEF.Services;
using WebshopEF.Controllers;

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
            container.RegisterType<AccountController>(new InjectionConstructor());

            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
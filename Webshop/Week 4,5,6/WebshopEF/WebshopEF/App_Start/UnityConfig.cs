using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using WebshopEF.Repositories;
using WebshopEF.Models;
using WebshopEF.BusinessLayer.Services;
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
            container.RegisterType<IDeviceService, DeviceService>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IBasketService, BasketService>();
            container.RegisterType<IBasketRepository, BasketRepository>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IOrderRepository, OrderRepository>();

            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
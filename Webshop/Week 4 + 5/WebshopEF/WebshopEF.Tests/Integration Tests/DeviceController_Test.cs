using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebshopEF.Controllers;
using WebshopEF.Models;
using WebshopEF.Repositories;
using WebshopEF.Services;
using WebshopEF.Tests.Database;

namespace WebshopEF.Tests.Integration_Tests
{
    class DeviceController_Test
    {
        [TestClass]
        public class CatalogController_Test
        {
            private DeviceController controller = null;
            private IProductService productService = null;
            private IGenericRepository<OS> repoOS = null;
            private IGenericRepository<Framework> repoFramework = null;
            private IDeviceRepository repoDevice = null;

            [TestInitialize]
            public void Setup()
            {
                new SetupDatabase().InitializeDatabase(new ApplicationDbContext());
                repoDevice = new DeviceRepository();
                repoFramework = new GenericRepository<Framework>();
                repoOS = new GenericRepository<OS>();
                productService = new ProductService(repoOS,repoFramework,repoDevice);
                controller = new DeviceController(productService);
            }

            [TestMethod]
            public void Index_Test()
            {
                //Act
                ViewResult result = (ViewResult)controller.Index();
                List<Device> devices = result.Model as List<Device>;

                //Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result.Model, typeof(List<Device>));
                Assert.IsTrue(devices.Count == 5);
            }
        }

    }
}

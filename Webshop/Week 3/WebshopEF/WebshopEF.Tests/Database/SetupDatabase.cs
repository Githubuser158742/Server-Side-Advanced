using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopEF.Models;
using System.Data.Entity.Migrations;

namespace WebshopEF.Tests.Database
{
    public class SetupDatabase : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private string pathDevices = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\App_Data\\Devices.txt";
        private string pathFrameworks = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\App_Data\\ProgrammingFramework.txt";
        private string pathOperatingSystems = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\App_Data\\Os.txt";

        List<Device> devices = new List<Device>();
        List<Framework> frameworks = new List<Framework>();
        List<OS> operatingSystems = new List<OS>();

        public override void InitializeDatabase(WebshopEF.Models.ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
            FillData(context);
        }

        public void FillData(ApplicationDbContext context)
        {
            seedRoles(context);
            seedFrameworks(context);
            seedOperatingSystems(context);
            seedDevices(context);
        }

        public void seedRoles(WebshopEF.Models.ApplicationDbContext context)
        {
            string roleAdmin = "Admin";
            string roleUser = "User";

            IdentityResult roleResult;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(roleAdmin))
            {
                roleResult = roleManager.Create(new IdentityRole(roleAdmin));
            }

            if (!roleManager.RoleExists(roleUser))
            {
                roleResult = roleManager.Create(new IdentityRole(roleUser));
            }

            if (!context.Users.Any(u => u.Email.Equals("jim.butseraen@gmail.com")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Butseraen",
                    FirstName = "Jim",
                    Email = "jim.butseraen@gmail.com",
                    UserName = "jim.butseraen@gmail.com",
                    Address = "GKG",
                    City = "Kortrijk",
                    ZipCode = "8500"
                };
                manager.Create(user, "-Password1");
                manager.AddToRole(user.Id, roleAdmin);
            }
        }
        public void seedFrameworks(WebshopEF.Models.ApplicationDbContext context)
        {
            using (StreamReader sr = new StreamReader(pathFrameworks))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    Framework f = new Framework();
                    f.ID = Convert.ToInt32(line.Split(';')[0]);
                    f.Name = Convert.ToString(line.Split(';')[1]);

                    frameworks.Add(f);

                    line = sr.ReadLine();
                }
                sr.Close();
            }
            foreach (Framework f in frameworks)
            {
                context.Framework.AddOrUpdate(f);
            }
            context.SaveChanges();
        }

        public void seedOperatingSystems(WebshopEF.Models.ApplicationDbContext context)
        {
            using (StreamReader sr = new StreamReader(pathOperatingSystems))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    OS o = new OS();

                    o.ID = Convert.ToInt32(line.Split(';')[0]);
                    o.Name = Convert.ToString(line.Split(';')[1]);

                    operatingSystems.Add(o);

                    line = sr.ReadLine();
                }
                sr.Close();
            }
            foreach (OS o in operatingSystems)
            {
                context.OS.AddOrUpdate(o);
            }
            context.SaveChanges();
        }

        public void seedDevices(WebshopEF.Models.ApplicationDbContext context)
        {
            using (StreamReader sr = new StreamReader(pathDevices))
            {
                sr.ReadLine();
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] items = line.Split(';');
                    Device d = new Device();
                    d.ID = Convert.ToInt32(items[0]);
                    d.Name = Convert.ToString(items[1]);
                    d.Price = Convert.ToDouble(items[2]);
                    d.RentPrice = Convert.ToDouble(items[3]);
                    d.Stock = Convert.ToInt32(items[4]);
                    d.Picture = Convert.ToString(items[5]);
                    d.OperatingSystems = new List<OS>();
                    d.Frameworks = new List<Framework>();
                    d.Description = Convert.ToString(items[8]);

                    string[] osItems = items[6].Split('-');
                    string[] frItems = items[7].Split('-');

                    if (osItems.Length > 0)
                    {
                        foreach (string item in osItems)
                        {
                            int id = Convert.ToInt32(item);
                            var query = (from o in context.OS
                                         where o.ID == id
                                         select o);
                            d.OperatingSystems.Add(query.SingleOrDefault<OS>());
                        }
                    }

                    if (frItems.Length > 0)
                    {
                        foreach (string item in frItems)
                        {
                            int id = Convert.ToInt32(item);
                            var query = (from f in context.Framework
                                         where f.ID == id
                                         select f);
                            d.Frameworks.Add(query.SingleOrDefault<Framework>());
                        }
                    }
                    devices.Add(d);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            foreach (Device d in devices)
            {
                context.Device.AddOrUpdate(d);
            }
            context.SaveChanges();
        }
    }
}

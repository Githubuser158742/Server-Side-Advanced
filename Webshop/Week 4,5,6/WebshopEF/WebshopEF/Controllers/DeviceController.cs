using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopEF.Models;
using WebshopEF.PresentationModels;
using WebshopEF.Repositories;
using WebshopEF.BusinessLayer.Services;

namespace WebshopEF.Controllers
{
    public class DeviceController : Controller
    {
        private IDeviceService ds;

        public DeviceController(IDeviceService ds)
        {
            this.ds = ds;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Device> devices = new List<Device>();
            devices = ds.GetDevices();
            return View(devices);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Device device = new Device();
            device = ds.GetDeviceById(id.Value);
            return View(device);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            CreatePM pm = new CreatePM();

            pm.OperatingSystems = new SelectList(ds.GetOperatingSystems(), "ID", "Name");
            pm.Frameworks = new SelectList(ds.GetFrameworks(), "ID", "Name");
            pm.SelectedOperatingSystems = new SelectList(new List<OS>(), "ID", "Name");
            pm.SelectedFrameworks = new SelectList(new List<Framework>(), "ID", "Name");
            pm.Device = new Device();

            return View(pm);
        }

        [HttpPost]
        public ActionResult Create(CreatePM createPM)
        {
            createPM.OperatingSystems = new SelectList(ds.GetOperatingSystems(), "ID", "Name");
            createPM.Frameworks = new SelectList(ds.GetFrameworks(), "ID", "Name");
            bool canComplete = false;

            switch(createPM.submit)
            {
                case "Next":
                    canComplete = true;
                    break;
                case "Add OS":
                    createPM.idsOS += createPM.OperatingSystemId.ToString()+";";
                    break;
                case "Add FR":
                    createPM.idsFR += createPM.FrameworkId.ToString()+";";
                    break;        
            }

            List<OS> OperatingSystems = CreateSelectedOS(createPM.idsOS);
            List<Framework> Frameworks = CreateSelectedFR(createPM.idsFR);

            if(Frameworks.Count!=0)createPM.SelectedFrameworks = new SelectList(Frameworks,"ID","Name");
            else createPM.SelectedFrameworks = new SelectList(new List<Framework>(),"ID","Name");

            if(OperatingSystems.Count!=0)createPM.SelectedOperatingSystems = new SelectList(OperatingSystems,"ID","Name");
            else createPM.SelectedOperatingSystems = new SelectList(new List<OperatingSystem>(),"ID","Name");

            if (ValidateForm(createPM))
            {
                if(canComplete)
                {
                    HttpPostedFileBase photo = createPM.Upload;
                    Device d = new Device();
                    d = createPM.Device;

                    if (createPM.Upload != null)
                    {
                        if (photo.ContentLength > 0)
                        {
                            ds.SaveImage(photo);
                            d.Picture = photo.FileName;
                        }
                    }

                    d.Frameworks = Frameworks;
                    d.OperatingSystems = OperatingSystems;
                    ds.CreateDevice(d);
                    return RedirectToAction("Index");
                }               
            }
            else
            {
                createPM.Error = "Complete all fields!";
            }
            return View(createPM);
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
                        list.Add(ds.GetOperatingSystemById(Convert.ToInt32(item)));
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
                        list.Add(ds.GetFrameworkById(Convert.ToInt32(item)));
                    }
                }
            }
            return list;
        }
    }
}
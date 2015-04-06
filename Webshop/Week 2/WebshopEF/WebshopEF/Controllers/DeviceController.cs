using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopEF.Models;
using WebshopEF.PresentationModels;
using WebshopEF.Repositories;

namespace WebshopEF.Controllers
{
    public class DeviceController : Controller
    {
        DeviceRepository dr = new DeviceRepository();
        FrameworkRepository fr = new FrameworkRepository();
        OperatingSystemRepository or = new OperatingSystemRepository();

        [HttpGet]
        public ActionResult Index()
        {
            List<Device> devices = new List<Device>();
            devices = dr.GetDevices();
            return View(devices);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Device device = new Device();
            device = dr.GetDeviceById(id);
            return View(device);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreatePM pm = new CreatePM();

            pm.OperatingSystems = new SelectList(or.GetOperatingSystems(), "ID", "Name");
            pm.Frameworks = new SelectList(fr.GetFrameworks(), "ID", "Name");
            pm.SelectedOperatingSystems = new SelectList(new List<OS>(), "ID", "Name");
            pm.SelectedFrameworks = new SelectList(new List<Framework>(), "ID", "Name");
            pm.Device = new Device();

            return View(pm);
        }

        [HttpPost]
        public ActionResult Create(CreatePM createPM)
        {
            createPM.OperatingSystems = new SelectList(or.GetOperatingSystems(), "ID", "Name");
            createPM.Frameworks = new SelectList(fr.GetFrameworks(), "ID", "Name");
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
                    if(createPM.Upload != null)
                    {
                        if(photo.ContentLength>0)
                        {
                            photo.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\WebshopEF\\Images\\",Path.GetFileName(photo.FileName)));
                        }
                    }
                    Device d = new Device();

                    d = createPM.Device;
                    d.Picture = Path.GetFileName(photo.FileName);
                    d.Frameworks = Frameworks;
                    d.OperatingSystems = OperatingSystems;
                    dr.CreateDevice(d);
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
            if (pm.Device.Name == string.Empty || pm.Device.Name == null) return false;
            if (pm.Device.Description == string.Empty || pm.Device.Description == null) return false;
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
                        list.Add(or.GetOperatingSystemById(Convert.ToInt32(item)));
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
                        list.Add(fr.GetFrameworkById(Convert.ToInt32(item)));
                    }
                }
            }
            return list;
        }
    }
}
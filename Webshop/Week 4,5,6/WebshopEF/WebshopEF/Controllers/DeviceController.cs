using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebshopEF.Models;
using WebshopEF.PresentationModels;
using WebshopEF.Repositories;
using WebshopEF.Services;

namespace WebshopEF.Controllers
{
    public class DeviceController : Controller
    {
        private IProductService ps;

        public DeviceController(IProductService ps)
        {
            this.ps = ps;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Device> devices = new List<Device>();
            devices = ps.GetDevices();
            return View(devices);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Device device = new Device();
            device = ps.GetDeviceById(id);
            return View(device);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            CreatePM pm = new CreatePM();

            pm.OperatingSystems = new SelectList(ps.GetOperatingSystems(), "ID", "Name");
            pm.Frameworks = new SelectList(ps.GetFrameworks(), "ID", "Name");
            pm.SelectedOperatingSystems = new SelectList(new List<OS>(), "ID", "Name");
            pm.SelectedFrameworks = new SelectList(new List<Framework>(), "ID", "Name");
            pm.Device = new Device();

            return View(pm);
        }

        [HttpPost]
        public ActionResult Create(CreatePM createPM)
        {
            createPM.OperatingSystems = new SelectList(ps.GetOperatingSystems(), "ID", "Name");
            createPM.Frameworks = new SelectList(ps.GetFrameworks(), "ID", "Name");
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

            List<OS> OperatingSystems = ps.CreateSelectedOS(createPM.idsOS);
            List<Framework> Frameworks = ps.CreateSelectedFR(createPM.idsFR);

            if(Frameworks.Count!=0)createPM.SelectedFrameworks = new SelectList(Frameworks,"ID","Name");
            else createPM.SelectedFrameworks = new SelectList(new List<Framework>(),"ID","Name");

            if(OperatingSystems.Count!=0)createPM.SelectedOperatingSystems = new SelectList(OperatingSystems,"ID","Name");
            else createPM.SelectedOperatingSystems = new SelectList(new List<OperatingSystem>(),"ID","Name");

            if (ps.ValidateForm(createPM))
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
                            ps.SaveImage(photo);
                            d.Picture = photo.FileName;
                        }
                    }

                    d.Frameworks = Frameworks;
                    d.OperatingSystems = OperatingSystems;
                    ps.CreateDevice(d);
                    return RedirectToAction("Index");
                }               
            }
            else
            {
                createPM.Error = "Complete all fields!";
            }
            return View(createPM);
        }       
    }
}
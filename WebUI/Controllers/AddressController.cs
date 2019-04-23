using ACP.BLL.Models;
using ACP.BLL.Modules;
using ACP.BLL.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        IGenericService<StreetDTO> streetService;
        IGenericService<AddressDTO> addressService;
        IGenericService<SubdivisionDTO> subdivService;

        public AddressController(IGenericService<StreetDTO> streetService,
            IGenericService<AddressDTO> addressService,
            IGenericService<SubdivisionDTO> subdivService)
        {
            this.streetService = streetService;
            this.addressService = addressService;
            this.subdivService = subdivService;
        }

        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            AddressDTO addrDTO;
            if (id == 0)
            {
                addrDTO = new AddressDTO();
      
            }
            else
            {
                addrDTO = addressService.GetAll().Where(d => d.AddressId == id).FirstOrDefault();
            }
            List<StreetDTO >lst = streetService.GetAll().ToList();

            ViewBag.Streets = new SelectList(lst, "StreetId", "StreetName", addrDTO.StreetId);
            ViewBag.Subdiv = new SelectList(subdivService.GetAll().ToList(), "SubdivisionId", "SubdivisionName", addrDTO.SubdivisionId);
            return View(addrDTO);
        }
        [HttpPost]
        public ActionResult Edit(AddressDTO addres)
        {
            if (ModelState.IsValid)
            {        
                addres.StreetId = Convert.ToInt32(Request["StreetName"]);
                addres.SubdivisionId = Convert.ToInt32(Request["SubdivisionName"]);
                addressService.Update(addres);      
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Index()
        {
            var model = addressService.GetAll().ToList();

            var streets = streetService.GetAll().Select(r => new { r.StreetId, r.StreetName }).ToList();
         
           ViewBag.StritId = new SelectList(streets, "StreetId", "StreetName");
            return View();
       
        }

        public ActionResult AddresByStreet(int id, int page = 1)
        {
            VwAddressByStreet data = new VwAddressByStreet(addressService, id, page);
            return PartialView(data);
        }


        public ActionResult Delete(int id)
        {    
            try
            {
                addressService.Delete(id);
                return Json("OK");
            }
            catch (Exception exc)
            {
                return Json("Bad");

            }

        }


    }
}
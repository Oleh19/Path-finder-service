using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ТеаmGoogleMap.Maps.Models;

namespace ТеаmGoogleMap.Maps.Controllers
{
    public class HomeController : Controller
    {
        IGenericService<Address> addressesService;
        IGenericService<Street> streetsService;
        IGenericService<Subdivision> subdivisionsService;
        TeamGoogleMapContext context;

        public HomeController()
        {
            context = new TeamGoogleMapContext();
            addressesService = new TempService<Address>();
            streetsService = new TempService<Street>();
            subdivisionsService = new TempService<Subdivision>();
        }


        [NonAction]
        public Type GetGeneticType(object obj, int num)
        {
            return obj.GetType().GetGenericArguments()[num - 1];
        }

        public ActionResult Index()
        {
            List<AddressVM> model = new List<AddressVM>();

            #region TypesStuff
            //Type addressType = GetGeneticType(addressesService, 1);
            //var addressInstance = Activator.CreateInstance(addressType);

            //Type streetInstance = GetGeneticType(streetsService, 1);
            //var streetType = Activator.CreateInstance(streetInstance);

            //Type subdivisionType = GetGeneticType(subdivisionsService, 1);
            //var subdivisionInstance = Activator.CreateInstance(subdivisionType);


            //Type listType = typeof(List<>);
            //var genericType = listType.MakeGenericType(addressType);
            //var instance = Activator.CreateInstance(genericType) as IList;

            //IList addresses = addressesService.GetAll() as IList;

            //for (int i = 0; i < addressesService.GetAll().Count(); i++)
            //{
            //    addressInstance = addresses[i];
            //    model.Add(new AddressVM
            //    {

            //    });
            //}
            #endregion
            foreach (var item in addressesService.GetAll())
            {
                model.Add(new AddressVM
                {
                    Id = item.AddressId,
                    House = item.House,
                    Latitude = item.Latitude.ToString().Replace(',', '.'),
                    Longitude = item.Longitude.ToString().Replace(',', '.'),
                    Serial = item.Serial,
                    StreetName = item.Street.StreetName,
                    SubdivisionName = item.Subdivision.SubdivisionName,
                    СountEntrance = item.СountEntrance,
                    СountFloor = item.СountFloor
                });
            }
            object output = JsonConvert.SerializeObject(model);
            return View(output);
        }

        [HttpPost]
        public ActionResult Autocomplete(string content)
        {
            var matches = addressesService
                .GetAll()
                .Where(x => content.ToLower().Contains(x.House) ||
                x.Street.StreetName.ToLower().Contains(content.ToLower()))
                .Select(x => new { id = x.StreetId, value = $"{x.Street.StreetName}, {x.House}" });
            object output = JsonConvert.SerializeObject(matches);
            return View(output);
        }
    }
}
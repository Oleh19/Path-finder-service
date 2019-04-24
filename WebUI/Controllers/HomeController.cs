using ACP.BLL.Models;
using ACP.BLL.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ТеаmGoogleMap.Maps.Models;

namespace ТеаmGoogleMap.Maps.Controllers
{
    public class HomeController : Controller
    {
        IGenericService<AddressDTO> addressesService;
        IGenericService<StreetDTO> streetsService;
        IGenericService<SubdivisionDTO> subdivisionsService;
        List<AddressDTO> addresses;

        public HomeController(IGenericService<AddressDTO> addressService,
                              IGenericService<StreetDTO> streetService,
                              IGenericService<SubdivisionDTO> subdivisionService)
        {
            this.addressesService = addressService;
            this.streetsService = streetService;
            this.subdivisionsService = subdivisionService;
            addresses = addressesService.GetAll().ToList();
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
                    StreetName = item.StreetName,
                    SubdivisionName = item.SubdivisionName,
                    СountEntrance = item.СountEntrance,
                    СountFloor = item.СountFloor
                });
            }
            object output = JsonConvert.SerializeObject(model);
            return View(output);
        }

        [HttpPost]
        public string Autocomplete(string content)
        {
            var matches = addresses
                .Where(x => content.ToLower().Contains(x.House.ToLower()) ||
                x.StreetName.ToLower().Contains(content.ToLower()))
                .Select(x => new { id = x.AddressId, value = $"{x.StreetName}, {x.House}", lat = x.Latitude, lon = x.Longitude }).Take(10);
            string output = JsonConvert.SerializeObject(matches);
            return output;
        }

        [HttpPost]
        public string ShowPlace(string content)
        {
            dynamic coords = addresses
                .Select(x => new
                {
                    id = x.AddressId,
                    value = $"{x.StreetName}, {x.House}",
                    lat = x.Latitude,
                    lon = x.Longitude
                })
                .Where(x => String.Compare(x.value, content) == 0).Select(x => new
                {
                    x.lat,
                    x.lon,
                    x.id,
                }).First();
            int cnt = -1;
            for (int i = 0; i < addresses.Count; i++)
            {
                if (addresses[i].AddressId == coords.id)
                    cnt = i;
            }
            string output = JsonConvert.SerializeObject(new Tuple<dynamic, int>(coords, cnt));
            return output;
        }
    }
}

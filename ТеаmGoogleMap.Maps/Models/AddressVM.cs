using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ТеаmGoogleMap.Maps.Models
{
    public class AddressVM
    {
        public int Id { get; set; }

        public string StreetName { get; set; }

        public string SubdivisionName { get; set; }

        public string House { get; set; }

        public string Serial { get; set; }

        public int? СountFloor { get; set; }

        public int? СountEntrance { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
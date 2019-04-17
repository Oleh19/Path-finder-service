using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ТеаmGoogleMap.Maps.Models;

namespace ForCoordinateFilling
{
    class Program
    {
        static void Main(string[] args)
        {
            TeamGoogleMapContext context = new TeamGoogleMapContext();
            //List<Address> addresses = new List<Address>(context.Addresses);
            ////////////////////////////////////////////////////////////////////////
            //for (int i = 0; i < 1; i++)
            //{
            //    WebRequest request = WebRequest.Create(
            //        "https://maps.googleapis.com/maps/api/geocode/json?address="
            //        + addresses[i].House +
            //        ","
            //        + addresses[i].Streets.StreetName +
            //        ","
            //        + ",Киев&key=AIzaSyC2x0YOLeRMePiIUr-aQFv7i31HZPdHWEo&callback");
            //    string text;
            //    request.ContentType = "application/json; charset=utf-8";
            //    var response = (HttpWebResponse)request.GetResponse();

            //    using (var sr = new StreamReader(response.GetResponseStream()))
            //    {
            //        text = sr.ReadToEnd();
            //    }
            //    Console.WriteLine(text);
            //    Console.WriteLine("==================================");
            //}
            ////////////////////////////////////////////////////////////////////////////////
            foreach (var item in context.Addresses)
            {
                var locationService = new GoogleLocationService("AIzaSyC2x0YOLeRMePiIUr-aQFv7i31HZPdHWEo&callback");
                AddressData ad = new AddressData();
                ad.Country = "Ukraine";
                ad.City = "Kyiv";

                ad.Address = item.Street.StreetName + " " + item.House;
                var point = locationService.GetLatLongFromAddress(ad);

                item.Longitude = Decimal.Parse(point.Longitude.ToString());
                item.Latitude = Decimal.Parse(point.Latitude.ToString());
                //Console.WriteLine(item.Longitude);
                //Console.WriteLine(item.Latitude);
                //context.SaveChanges();
                //Console.WriteLine(item.Longitude);
                //Console.WriteLine(item.Latitude);
                //Console.ReadKey();
                //Thread.Sleep(10);
            }
            Console.WriteLine("DONE");
            context.SaveChanges();
            //foreach (var item in context.Addresses)
            //{
            //    Console.WriteLine(item.Longitude);
            //}
            //context.SaveChanges();
            /////////////////////////////////////////////////////////////////////////////////////
            //List<Address> addresses = new List<Address>(context.Addresses);
            //for (int i = 1; i < context.Addresses.Count() + 1; i++)
            //{
            //    var locationService = new GoogleLocationService("AIzaSyC2x0YOLeRMePiIUr-aQFv7i31HZPdHWEo&callback");
            //    AddressData ad = new AddressData();
            //    ad.Country = "Ukraine";
            //    ad.City = "Kyiv";

            //    ad.Address = addresses[i].Street.StreetName + " " + addresses[i].House;
            //    var point = locationService.GetLatLongFromAddress(ad);

            //    context.Addresses.Find(addresses[i].AddressId).Longitude = Decimal.Parse(point.Longitude.ToString());
            //    context.Addresses.Find(addresses[i].AddressId).Latitude = Decimal.Parse(point.Latitude.ToString());
            //    Console.WriteLine(context.Addresses.Find(addresses[i].AddressId).Longitude);
            //    Console.WriteLine(context.Addresses.Find(addresses[i].AddressId).Latitude);
            //    context.SaveChanges();
            //    Console.WriteLine(context.Addresses.Find(addresses[i].AddressId).Longitude);
            //    Console.WriteLine(context.Addresses.Find(addresses[i].AddressId).Latitude);
            //    Console.ReadKey();
            //    Thread.Sleep(100);
            //}
        }
    }
}

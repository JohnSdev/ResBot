using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ReseBot.Dialogs
{
    public class Sl
    {
        public static Tuple<string, string> slPlace(string place)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://api.sl.se/api2/typeahead.json?key=&searchstring={place}&maxresults=1");// Add keys for API call
            request.Method = "Get";
            request.KeepAlive = true;
            //request.ContentType = "appication/json";
            //request.Headers.Add("Content-Type", "appication/json");
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }
            var data = (JObject)JsonConvert.DeserializeObject(myResponse);
            string name = data["ResponseData"][0]["Name"].ToString();
            string siteid = data["ResponseData"][0]["SiteId"].ToString();

            return new Tuple<string, string>(name, siteid);

        }

        public static string formatter(JToken section)
        {
            string r1 = "";
            string dir = "direction";
            if (section["dist"] != null)
            {
                dir = "type";
                r1 = section["Origin"]["time"].ToString().Substring(0, 5) + " Gå " + section["dist"] + "m > " + section["Destination"]["name"];

            }
            else r1 = section["Origin"]["time"].ToString().Substring(0, 5) + " " + section["name"] + " > " + section[dir];

            return r1;
        }

        public static Tuple<string, List<string>> slRoute(string start, string end, string endname)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://api.sl.se/api2/TravelplannerV3/trip.json?key=&originExtId={start}&destExtId={end}");// Add key after Key=
            request.Method = "Get";
            request.KeepAlive = true;
            //request.ContentType = "appication/json";
            //request.Headers.Add("Content-Type", "appication/json");
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string myResponse = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                myResponse = sr.ReadToEnd();
            }
            var lista2 = new List<string>();
 
            return new Tuple<string, List<string>>(myResponse, lista2);
        }

    }
}
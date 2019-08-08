using System;
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Web.Mvc;using System.IO;
using System.Web;

namespace SmartSeat.Models
{
    public class DweetService
    {

        public class Content
        {
            public DateTime Time { get; set; }
            public int Dust { get; set; }
            public int AQI { get; set; }
            public int Temp { get; set; }
            public int Hum { get; set; }
            public double Lat { get; set; }
            public double Lon { get; set; }
        }

        public class With
        {
            public string thing { get; set; }
            public DateTime created { get; set; }
            public Content content { get; set; }
        }
        public class JsonDweet
        {
            public string @this { get; set; }
            public string by { get; set; }
            public string the { get; set; }
            public List<With> with { get; set; }
        }




        public static JsonDweet checkDweetValue()
        {

            //string url = ConfigurationManager.AppSettings["UrlDweet"];
            string url = "";
            if (!String.IsNullOrEmpty(HttpContext.Current.Session["station"].ToString()))
            {
                url = "https://dweet.io/get/latest/dweet/for/" + HttpContext.Current.Session["station"].ToString();
            }else
            {
                url = "https://dweet.io/get/latest/dweet/for/THDust_001";
            }

            StringContent Content = new StringContent("application/json");
            using (var client = new HttpClient())
            {
                JsonDweet returnObject = new JsonDweet();
                try
                {
                    HttpResponseMessage Response = client.GetAsync(url).Result;
                    string str = Response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(Response.StatusCode);
                    returnObject = JsonConvert.DeserializeObject<JsonDweet>(str);
                    return returnObject;
                }
                catch
                {
                    return new JsonDweet();
                }
            }
        }

    }
}
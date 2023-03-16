using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Encodings.Web;
using static WeatherForecast.Models.Weather;
using Newtonsoft.Json;

namespace WeatherForecast.Controllers
{
    public class WeatherFetch : Controller
    {
        // 
        // GET: /WeatherFetch/
        public IActionResult Index(string q)
        {
            string APIkey = "0cd9db1aa08c4c14949230244231303";
            string uri = string.Format("https://api.weatherapi.com/v1/forecast.json?q={0}&days=14&key={1}", q, APIkey);
            Debug.WriteLine("fetching data from ", uri);
            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(uri);
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(json);
                //List<string> condList = new List<string>();
                //foreach (var item in myDeserializedClass.forecast.forecastday)
                //{
                //    condList.Add(item.day.condition.text);
                //}
                //ViewData["temp"] = condList;
                ViewData["temp"] = myDeserializedClass.forecast.forecastday;
                ViewData["curr"] = myDeserializedClass.current;
                ViewData["loc"] = myDeserializedClass.location;
                return View();
            }
        }
    }
}

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
            string sun = "sun";
            string snow = "snow";
            string cloud = "cloud";
            string rain = "rain";
            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(uri);
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(json);

                if (myDeserializedClass.current.condition.text.ToLower().Contains(sun))
                {
                    ViewData["color"] = "#e3dd36"; 
                }
                if (myDeserializedClass.current.condition.text.ToLower().Contains(snow))
                {
                    ViewData["color"] = "#FFFFFF";
                }
                if (myDeserializedClass.current.condition.text.ToLower().Contains(rain))
                {
                    ViewData["color"] = "#528079";
                }
                if (myDeserializedClass.current.condition.text.ToLower().Contains(cloud) || myDeserializedClass.current.condition.text.ToLower().Contains("mist"))
                {
                    ViewData["color"] = "#b1b5b5";
                }
                ViewData["temp"] = myDeserializedClass.forecast.forecastday;
                ViewData["curr"] = myDeserializedClass.current;
                ViewData["loc"] = myDeserializedClass.location;
                return View();
            }
        }
    }
}

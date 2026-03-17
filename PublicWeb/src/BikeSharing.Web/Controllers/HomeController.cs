using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;

namespace BikeSharing.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly TelemetryClient _telemetry = new TelemetryClient();

        private static readonly HashSet<string> ValidCities = new HashSet<string>
        {
            "NewYork", "Seattle", "SanFrancisco", "Boston", "Barcelona", "MexicoCity"
        };

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/City?name=Seattle
        public ActionResult City(string name)
        {
            if (string.IsNullOrEmpty(name) || !ValidCities.Contains(name))
            {
                return RedirectToAction("Index");
            }

            _telemetry.TrackEvent("CitySelected", new Dictionary<string, string>
            {
                { "CityName", name }
            });

            ViewBag.CityName = name;
            ViewBag.CityDisplayName = GetDisplayName(name);

            return View();
        }

        private static string GetDisplayName(string name)
        {
            switch (name)
            {
                case "NewYork": return Resources.Locale.Cities_NewYork;
                case "Seattle": return Resources.Locale.Cities_Seattle;
                case "SanFrancisco": return Resources.Locale.Cities_SanFrancisco;
                case "Boston": return Resources.Locale.Cities_Boston;
                case "Barcelona": return Resources.Locale.Cities_Barcelona;
                case "MexicoCity": return Resources.Locale.Cities_MexicoCity;
                default: return name;
            }
        }
    }
}
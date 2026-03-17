using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.ApplicationInsights.Extensibility;

namespace BikeSharing.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["ApplicationInsights:ConnectionString"];
            if (!string.IsNullOrEmpty(connectionString))
            {
                TelemetryConfiguration.Active.ConnectionString = connectionString;
            }

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}

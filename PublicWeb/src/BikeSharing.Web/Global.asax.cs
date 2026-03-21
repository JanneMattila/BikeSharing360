using System;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.ApplicationInsights.Extensibility;

namespace BikeSharing.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var backendUrl = ConfigurationManager.AppSettings["PrivateWebsite"];
            if (!string.IsNullOrEmpty(backendUrl))
            {
                // Check if the backend is up and running before starting the application
                var request = WebRequest.CreateHttp(backendUrl);
                request.Timeout = 5000;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Backend server OK");
                    }
                }
            }

            var connectionString = System.Configuration.ConfigurationManager.AppSettings["APPLICATIONINSIGHTS_CONNECTION_STRING"];
            if (!string.IsNullOrEmpty(connectionString))
            {
                TelemetryConfiguration.Active.ConnectionString = connectionString;
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}

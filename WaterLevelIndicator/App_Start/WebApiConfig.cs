using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace WaterLevelIndicator
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "WaterLevelApi",
                routeTemplate: "api/waterlevel/{selectedLabel}",
                defaults: new { controller = "WaterLevelApi", action = "GetWaterLevelData", selectedLabel = RouteParameter.Optional }
            );
        }
    }
}

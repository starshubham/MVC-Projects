using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LayoutViewsExample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Resolving Routes Conflicts using string Literals
            //routes.MapRoute(
            //    name: "products",
            //    url: "products/GetProductId/{productName}",
            //    defaults: new { controller = "products", action = "GetProductId" }
            //);

            // Resolving Routes Conflicts using Route Constraints
            //routes.MapRoute(
            //    name: "products",
            //    url: "{controller}/{action}/{productName}",
            //    defaults: new { },
            //    constraints: new { productName = @"^[A-Za-z ]*$"}
            //);

            // Attribute Routing
            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);            
        }
    }
}

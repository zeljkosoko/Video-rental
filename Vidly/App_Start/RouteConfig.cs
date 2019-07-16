using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes(); //for adding [Route(movies/...)]

            //custom route first then default
            //routes.MapRoute(
            //    "MoviesByReleasedDate",
            //    "movies/released/{year}/{month}",
            //    new { controler = "Movies", action = "ByReleaseDate"},
            //    new { year = @"\d{4}", month = @"\d{2}"} );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

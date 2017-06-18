using System.Web.Mvc;
using System.Web.Routing;

namespace ImageSeeds
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Upload", "Upload", new {controller = "Home", action = "Upload"}
                );

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Account", action = "Login", id = UrlParameter.Optional}
                );
        }
    }
}
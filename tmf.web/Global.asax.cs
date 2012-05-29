using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using tmf.business;
using tmf.web.Models;

namespace tmf.web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            var configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            var connectionString = configuration.ConnectionStrings.ConnectionStrings["tmfwebContext"].ConnectionString;
            if (!connectionString.Contains("MultipleActiveResultSets=True;"))
            {
                connectionString += "MultipleActiveResultSets=True;";
            }

            configuration.ConnectionStrings.ConnectionStrings["tmfwebContext"].ConnectionString = connectionString;
            configuration.Save();

            Database.SetInitializer(new DataContextInitializer());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
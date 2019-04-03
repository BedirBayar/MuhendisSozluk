using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace MuhendisSozluk
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

          //  RouteTable.Routes.Add("default", new Route("{title}", new PageRouteHandler("~/")));
            RouteTable.Routes.Add("baslik", new Route("{title}", new PageRouteHandler("~/default.aspx")));
            RouteTable.Routes.Add("yazar", new Route("yazar/{name}", new PageRouteHandler("~/user/yazar.aspx")));
            RouteTable.Routes.Add("entry", new Route("entry/{number}", new PageRouteHandler("~/entry/entry.aspx")));
            RouteTable.Routes.Add("oda", new Route("oda/{dep}", new PageRouteHandler("~/department/oda.aspx")));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }
        public static void RegisterRoute(System.Web.Routing.RouteCollection routes)
        {
            //routes.MapPageRoute("ForTitle", "~/{Name}", "~/default.aspx");
            //routes.MapPageRoute("ForTitle", "~/{Name}", "~/");
  
            routes.MapPageRoute("baslik", "/{title}", "~/default.aspx", false);
            routes.MapPageRoute("yazar", "yazar/{name}", "~/user/yazar.aspx", false);
            routes.MapPageRoute("entry", "entry/{number}", "~/entry/entry.aspx", false);
            routes.MapPageRoute("oda", "oda/{dep}", "~/department/oda.aspx", false);

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
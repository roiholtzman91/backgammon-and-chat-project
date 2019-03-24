using DalFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace shoppingsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        ProductRepo pr = new ProductRepo();        

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            pr.CheckTimeInCart();
            Timer t = new Timer();
            t.Interval = 1_000 * 120;
            t.Elapsed += Elap;
            t.Start();
        }
        private void Elap(object obj, ElapsedEventArgs e)
        {
            pr.CheckTimeInCart();
        }

    }
}

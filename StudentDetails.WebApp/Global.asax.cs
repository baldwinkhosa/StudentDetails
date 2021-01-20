using Ninject;
using Ninject.Web.Common.WebHost;
using StudentDetails.WebApp.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentDetails.WebApp
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected override IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel(new NinjectBindings());
            return kernel;
        }
    }
}

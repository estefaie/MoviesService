using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace MoviesService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            //container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Singleton);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            // Here your usual Web API configuration stuff.
        }
    }
}

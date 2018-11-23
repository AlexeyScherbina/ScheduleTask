using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using ScheduleTask.BLL.Interfaces;
using ScheduleTask.BLL.Services;
using ScheduleTask.DAL;
using ScheduleTask.DAL.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;


namespace Task1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {


            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();






            container.Register<IApplicationDbContext>(() => new ApplicationDbContext(), Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(IRepository<>).Assembly, Lifestyle.Scoped);
            container.Register(typeof(IUserService), typeof(UserService), Lifestyle.Scoped);
            container.Register(typeof(ITaskService), typeof(TaskService), Lifestyle.Scoped);

            container.Options.AllowOverridingRegistrations = true;

            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()), Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(
                () => new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext())), Lifestyle.Scoped
            );
            container.Register<UserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()), Lifestyle.Scoped);
            container.Register<UserManager<ApplicationUser, string>>(
                () => new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(new ApplicationDbContext())),
                Lifestyle.Scoped);

            container.Register<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>(Lifestyle.Scoped);
            container.Register<ITextEncoder, Base64UrlTextEncoder>(Lifestyle.Scoped);
            container.Register<IDataSerializer<AuthenticationTicket>, TicketSerializer>(Lifestyle.Scoped);
            container.Register<IDataProtector>(() => new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider().Create("ASP.NET Identity"), Lifestyle.Scoped);
            //container.Register(() => HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Scoped);
            container.Register<UserManager<ApplicationUser>>(
                () => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())),
                Lifestyle.Scoped);

            container.Options.AllowOverridingRegistrations = false;

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}

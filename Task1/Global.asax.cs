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
using ScheduleTask.BLL.Interfaces;
using ScheduleTask.BLL.Services;
using ScheduleTask.DAL;
using ScheduleTask.DAL.Interfaces;
using ScheduleTask.DAL.Repositories;
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


            container.Register<IApplicationDbContext, ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IDataAccess,DataAccess>(Lifestyle.Scoped);
            container.Register(typeof(IUserRepository), typeof(UserRepository), Lifestyle.Scoped);
            container.Register(typeof(ITaskRepository), typeof(TaskRepository), Lifestyle.Scoped);
            container.Register(typeof(IUserService), typeof(UserService), Lifestyle.Scoped);
            container.Register(typeof(ITaskService), typeof(TaskService), Lifestyle.Scoped);

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

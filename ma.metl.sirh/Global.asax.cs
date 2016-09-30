using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation;
using ma.metl.sirh.Modules;
using FluentValidation.Mvc;
using log4net.Config;
using System.Data.Entity;
using ma.metl.sirh.Model;
using ma.metl.sirh.Service;
using System.Globalization;
using System.Threading;
using ma.metl.sirh.App_Start;
using System.Web.Optimization;




namespace ma.metl.sirh
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);

        //    //Autofac Configuration
        //    var builder = new Autofac.ContainerBuilder();

        //    builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

        //    builder.RegisterModule(new RepositoryModule());
        //    builder.RegisterModule(new ServiceModule());
        //    builder.RegisterModule(new EFModule());

        //    var container = builder.Build();

        //    DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        //}
        
        protected void Application_Start()
        {
             
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
          //  UnityConfig.RegisterTypes();
            FluentValidationModelValidatorProvider.Configure();
            XmlConfigurator.Configure();
            MonitorConfig.RegisterWatchers();
            ModelBinders.Binders.DefaultBinder = new MyDefaultModelBinder();
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //permet de créer la base de données
           //Database.SetInitializer<sirhContext>(null);

           
        }
    }
}

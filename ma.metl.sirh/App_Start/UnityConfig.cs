using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using ma.metl.sirh.Modules;
using ma.metl.sirh.App_Start;
using System;
using System.Data.Entity;
using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using ma.metl.sirh.Service;

namespace ma.metl.sirh
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)        {


            container.AddNewExtension<RepositoryModule>();
            container.AddNewExtension<ServiceModule>();
            container.AddNewExtension<EFModule>();

            //container.RegisterType<DbContext, SampleArchContext>(new PerRequestLifetimeManager());
            //container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            //container.RegisterType<ICountryRepository, CountryRepository>();
            //container.RegisterType<IPersonRepository, PersonRepository>();
            //container.RegisterType<ICountryService, CountryService>();
            //container.RegisterType<IPersonService, PersonService>(); 
        
            
            ControllerBuilder.Current.SetControllerFactory(new MyUnityControllerFactory(container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
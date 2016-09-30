using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace ma.metl.sirh.GipeOrd
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<IGipeOrdService, GipeOrdService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
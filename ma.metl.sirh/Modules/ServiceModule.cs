using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

using Microsoft.Practices.Unity;

namespace ma.metl.sirh.Modules
{
   public class ServiceModule : UnityContainerExtension
    {

        protected override void Initialize()
        {

            Container.RegisterTypes(
            AllClasses.FromLoadedAssemblies().Where(t => t.Namespace == "ma.metl.sirh.Service"),
            WithMappings.FromMatchingInterface,
            WithName.Default,
            WithLifetime.None);

        }

    }
}
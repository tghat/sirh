using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

using Microsoft.Practices.Unity;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Modules
{
    public class RepositoryModule : UnityContainerExtension
    {
      
        protected override void Initialize()
        {

                  Container.RegisterTypes(
                  AllClasses.FromLoadedAssemblies().Where(t => t.Namespace == "ma.metl.sirh.Repository"),
                  WithMappings.FromMatchingInterface,
                  WithName.Default,
                  WithLifetime.None);

        }

    }
}
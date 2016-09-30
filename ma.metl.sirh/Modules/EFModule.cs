using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Ora;
using ma.metl.sirh.Repository;
using Microsoft.Practices.Unity;
using ma.metl.sirh.Repository.Common;


namespace ma.metl.sirh.Modules
{
        
    public class EFModule : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<RepositoryModule>();
            Container.RegisterType<DbContext, sirhContext>(new PerRequestLifetimeManager());
            Container.RegisterType<DbContext, GipeOrdContext>(new PerRequestLifetimeManager());
            Container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            Container.RegisterType<IUnitOfWorkOrd, UnitOfWorkOrd>(new PerRequestLifetimeManager());
            
        }

    }
}
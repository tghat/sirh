using ma.metl.sirh.Common.Elmah;
using ma.metl.sirh.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ma.metl.sirh.DI
{
    internal class InjectableControllerFactory : DefaultControllerFactory
    {

        private readonly IDependencyInjectionContainer container;
        public static Func<Type, object> CreateDependencyCallback = type => Activator.CreateInstance(type);

        public InjectableControllerFactory(IDependencyInjectionContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {

            try
            {
                if (controllerType == null)
                    return base.GetControllerInstance(requestContext, controllerType);
            }
            catch (HttpException ex)
            {
                if (ex.GetHttpCode() == 404)
                {
                    var errorController = (IController)CreateDependencyCallback(typeof(ErrorController));
                    ((ErrorController)errorController).HandleHttpException(requestContext.HttpContext, 404);
                    return errorController;
                }

                throw ex;
            }


            return container.GetInstance(controllerType) as IController;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = base.CreateController(requestContext, controllerName);

            var c = controller as Controller;

            if (c != null)
            {
                c.ActionInvoker = new ErrorHandlingActionInvoker(new HandleErrorWithELMAHAttribute());
            }

            return controller;
        }


        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }
    }
}
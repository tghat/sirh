using log4net;
using ma.metl.sirh.Common.TraceHistory;
using ma.metl.sirh.DI;
using ma.metl.sirh.Model;
using ma.metl.sirh.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ma.metl.sirh.Controllers
{
    public class BaseController : Controller
    {
        IHistoriqueService historiqueService;
        IDetailAvancementService detailAvancementService;

        protected readonly ILog logger = LogManager.GetLogger("BaseController");

        public BaseController(IHistoriqueService historiqueService, IDetailAvancementService detailAvancementService)
        {
            this.historiqueService = historiqueService;
            this.detailAvancementService = detailAvancementService;
        }

        public BaseController()
        {

        }

        protected override void HandleUnknownAction(string actionName)
        {
            // Ne pas boucler s'il y a des exceptions dans ErrorController
            if (GetType() != typeof(ErrorController))
                HandleHttpException(HttpContext, 404);
        }

        public ActionResult HandleHttpException(HttpContextBase httpContext, int statusCode)
        {
            var errorController = (IController)InjectableControllerFactory.CreateDependencyCallback(typeof(ErrorController));
            var errorRoute = new RouteData();

            switch (statusCode)
            {
                case 404:
                    errorRoute.Values.Add("controller", "Error");
                    errorRoute.Values.Add("action", "Http404");
                    logger.Error("Erreur 404 : " + httpContext.Request.Url.OriginalString);
                    break;
                case 500:
                    errorRoute.Values.Add("controller", "Error");
                    errorRoute.Values.Add("action", "Http500");
                    logger.Error("Erreur 500 à : " + httpContext.Request.Url.OriginalString);
                    break;
                default:
                    errorRoute.Values.Add("controller", "Error");
                    errorRoute.Values.Add("action", "Generic");
                    errorRoute.Values.Add("statusCode", statusCode);
                    logger.Error("Autres Erreurs : " + httpContext.Request.Url.OriginalString);
                    break;
            }

            errorRoute.Values.Add("url", httpContext.Request.Url.OriginalString);
            errorController.Execute(new RequestContext(httpContext, errorRoute));

            return new EmptyResult();
        }

        
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            String actionName = "";
            LogTraceAttribute logAttribute;

            var controllerActionDescriptor = filterContext.ActionDescriptor;

            foreach (Attribute attr in controllerActionDescriptor.GetCustomAttributes(typeof(LogTraceAttribute), false))
            {
                if (attr is LogTraceAttribute)
                {
                    Users user = (Users)Session["Users"];
                    
                    long? userId = null;
                   // int? userId = null;
                   // if (filterContext.HttpContext.GetOwinContext().Authentication.User.Identities.FirstOrDefault().IsAuthenticated)
                    //{
                     //   var user = filterContext.HttpContext.GetOwinContext().Authentication.User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));
                        if (!(user == null))
                        {
                            userId = user.Id;
                        }
                   // }

                    List<long> listId = (List<long>)filterContext.Controller.ViewData["listId"];

                    if(listId != null)
                    { 
                    foreach (long id in listId)
                    {
                        logAttribute = (LogTraceAttribute)attr;
                        actionName = logAttribute.Action;
                        Historique historique = new Historique();
                        historique.UserId = userId;
                        historique.Action = actionName;
                        historique.DetailId = id; 
                        historique.DateAction = DateTime.Now;
                        historiqueService.Create(historique);
                    }
                    }
                }
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;

            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();


            logger.Error("Erreur dans  " + controllerName + "->" + actionName + " : " + e.Message, e);

            filterContext.ExceptionHandled = true;

        }
    }
}
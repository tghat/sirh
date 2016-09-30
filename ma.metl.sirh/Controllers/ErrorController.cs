using ma.metl.sirh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ma.metl.sirh.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Http404(string url)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            var model = GetModel(url);
            return View("404", model);
        }

        public ActionResult Http500(string url)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var model = GetModel(url);
            return View("500", model);
        }

        public ActionResult Generic(string url, int statusCode)
        {
            Response.StatusCode = statusCode;
            var model = GetModel(url);
            model.StatusCode = statusCode;
            return View("Error", model);
        }

        private ErrorViewModel GetModel(string url)
        {
            var model = new ErrorViewModel();

            model.RequestedUrl = Request.Url.OriginalString.Contains(url) & Request.Url.OriginalString != url ? Request.Url.OriginalString : url;

            return model;
        }
    }
}
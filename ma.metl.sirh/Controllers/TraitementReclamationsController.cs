using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ma.metl.sirh.Controllers
{
    public class TraitementReclamationsController : Controller
    {
        //
        // GET: /TraitementReclamation/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TraiterReclamationFauxProprietaire()
        {
            return View();
        }

        public ActionResult TraiterReclamationCession()
        {
            return View();
        }

        public ActionResult TraiterReclamationDeclaration()
        {
            return View();
        }
	}
}
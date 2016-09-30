using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ma.metl.sirh.Controllers
{
    public class InterfacageGipeOrdController : Controller
    {
        readonly ICandidatService _candidatService;
        readonly IGradeService _gradeService;

        public InterfacageGipeOrdController(ICandidatService candidatService, IGradeService gradeService)
        {
            _candidatService = candidatService;
            _gradeService = gradeService;
        }  

        // GET: InterfacageGipeOrd
        public ActionResult Index()
        {
            return RedirectToAction("PreparationActe");
        }

        // GET: InterfacageGipeOrd/PreparationActe
        public ActionResult PreparationActe()
        {
            List<String> listAnnees = new List<string>();
            List<String> listStatuts = new List<string>();

            var valuesAnnees = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            var valuesStatut = Enum.GetValues(typeof(EtatDetailAVC)).Cast<EtatDetailAVC>();

            foreach (var r in valuesAnnees)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnees.Add(attributes[0].Description.ToString());
            }
            foreach (var r in valuesStatut)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listStatuts.Add(attributes[0].Description.ToString());
            }

            ViewBag.Annees = new SelectList(listAnnees);
            ViewBag.Grades = new SelectList(_gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");
            ViewBag.Status = new SelectList(listStatuts,"Retenu");

            var candidtats = _candidatService.GetCandidatsByCriteria(null,null,"retenu");

            Tuple<
                InterfacageActeCandidatRechercheVM, 
                List<InterfacageActeCandidatResultatVM>> tuple = new Tuple<
                    InterfacageActeCandidatRechercheVM, 
                    List<InterfacageActeCandidatResultatVM>>(
                        new InterfacageActeCandidatRechercheVM(),
                        candidtats);

            return View(tuple);
        }

        // POST: InterfacageGipeOrd/RechercherCandidats
        [HttpPost]
        public ActionResult RechercherCandidats( Tuple<InterfacageActeCandidatRechercheVM,List<InterfacageActeCandidatResultatVM>> model )
        {
            List<String> listAnnees = new List<string>();
            List<String> listStatuts = new List<string>();

            var valuesAnnees = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            var valuesStatut = Enum.GetValues(typeof(EtatDetailAVC)).Cast<EtatDetailAVC>();

            foreach (var r in valuesAnnees)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnees.Add(attributes[0].Description.ToString());
            }
            foreach (var r in valuesStatut)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listStatuts.Add(attributes[0].Description.ToString());
            }

            ViewBag.Annees = new SelectList(listAnnees);
            ViewBag.Grades = new SelectList(_gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");
            ViewBag.Status = new SelectList(listStatuts);

            var candidtats = _candidatService.GetCandidatsByCriteria(model.Item1.Annee, model.Item1.GradeId, model.Item1.Statut);

            Tuple<
                InterfacageActeCandidatRechercheVM,
                List<InterfacageActeCandidatResultatVM>> tuple = new Tuple<
                    InterfacageActeCandidatRechercheVM,
                    List<InterfacageActeCandidatResultatVM>>(
                        model.Item1,
                        candidtats);

            return View("PreparationActe", tuple);
        }

        // GET: InterfacageGipeOrd/Rapports
        public ActionResult Rapports()
        {
            return View();
        }
    }
}
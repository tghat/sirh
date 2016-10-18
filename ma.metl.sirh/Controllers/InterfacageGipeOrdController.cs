using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using ma.metl.sirh.Service;
using ma.metl.sirh.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ma.metl.sirh.Controllers
{
    public class InterfacageGipeOrdController : Controller
    {
        readonly ICandidatService _candidatService;
        readonly IGradeService _gradeService;
        readonly IActeGOService _acteGOService;
        readonly IUserGOService _userGOService;
        readonly IActeEventHistGOService _acteEventHistGOService;

        public InterfacageGipeOrdController(
            ICandidatService candidatService, 
            IGradeService gradeService, 
            IActeGOService acteGOService, 
            IUserGOService userGOService,
            IActeEventHistGOService acteEventHistGOService)
        {
            _candidatService = candidatService;
            _gradeService = gradeService;
            _acteGOService = acteGOService;
            _userGOService = userGOService;
            _acteEventHistGOService = acteEventHistGOService;
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

        public ActionResult RenderSynthes(int numDotti) {

            var detailsActeView = new DetailsActeViewModel();
            var historiqueView = new List<HistoriqueViewModel>();

            var acte = _acteGOService.GetLastActeByNumDotti(numDotti);

            if (acte != null)
            {
                detailsActeView.Ref_arrete = acte.REF_ARRETE.ToString();
                detailsActeView.Ann_visa = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(acte.ANN_VISA).ToString();
                detailsActeView.Status = GetStatusById(acte.STADE);
                detailsActeView.isAvailable = true;
            }

            var history_list = _acteEventHistGOService.GetActeEventsHistory(numDotti);

            foreach (var item in history_list) {
                historiqueView.Add(new HistoriqueViewModel()
                {
                    Status = GetStatusById(item.ACTE_STADE),
                    User = GetUserLibByCode(item.CREATED_BY),
                    DateAction = item.CREATION_DATE.Value.ToShortDateString()
                });
            }

            Tuple<
                DetailsActeViewModel,
                List<HistoriqueViewModel>> tuple = new Tuple<
                    DetailsActeViewModel,
                    List<HistoriqueViewModel>>(
                        detailsActeView,
                        historiqueView);

            return PartialView("SyntheseActe", tuple);

        }

        // GET: InterfacageGipeOrd/Rapports
        public ActionResult Rapports()
        {
            List<SelectListItem> operations = new List<SelectListItem>();

            operations.Add(new SelectListItem { Text = "Préparation", Value = "1" });
            operations.Add(new SelectListItem { Text = "Contrôlé", Value = "2" });
            operations.Add(new SelectListItem { Text = "Signé", Value = "3" });
            operations.Add(new SelectListItem { Text = "Envoyé", Value = "4" });
            operations.Add(new SelectListItem { Text = "Visé", Value = "5" });

            ViewBag.Users = new SelectList(_userGOService.GetAllUsers().OrderBy(x => x.LIB_USR), "COD_USR", "LIB_USR");
            ViewBag.Grades = new SelectList(_gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");
            ViewBag.Operations = operations;

            var rapportList = (from h in _acteEventHistGOService.GetAllActeEventHist()
                                 select new ma.metl.sirh.Model.Dto.RapportViewModel() { 
                                    Acte = h.ACTE_REF_ARR + "/" + CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(h.ACTE_ANN_VISA).ToString(),
                                    Action = GetStatusById(h.ACTE_STADE),
                                    User = GetUserLibByCode(h.CREATED_BY),
                                    ActionDate = h.CREATION_DATE.Value.ToShortDateString()
                                 }).ToList();

            Tuple<
                CritereRapportViewModel, 
                List<RapportViewModel>> tuple = new Tuple<
                    CritereRapportViewModel, 
                    List<RapportViewModel>>(
                        new CritereRapportViewModel(),
                        rapportList);

            return View(tuple);
        }

        // POST: InterfacageGipeOrd/Rapports
        [HttpPost]
        public ActionResult Rapports(Tuple<ma.metl.sirh.Model.Dto.CritereRapportViewModel,List<ma.metl.sirh.Model.Dto.RapportViewModel>> model)
        {
            List<SelectListItem> operations = new List<SelectListItem>();

            operations.Add(new SelectListItem { Text = "Préparation", Value = "1" });
            operations.Add(new SelectListItem { Text = "Contrôlé", Value = "2" });
            operations.Add(new SelectListItem { Text = "Signé", Value = "3" });
            operations.Add(new SelectListItem { Text = "Envoyé", Value = "4" });
            operations.Add(new SelectListItem { Text = "Visé", Value = "5" });

            ViewBag.Users = new SelectList(_userGOService.GetAllUsers().OrderBy(x => x.LIB_USR), "COD_USR", "LIB_USR");
            ViewBag.Grades = new SelectList(_gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");
            ViewBag.Operations = operations;

            var rapportList = (from r in _acteEventHistGOService.GetActeEventHistByCriteria(
                model.Item1.UserCode,
                model.Item1.GradeId,
                model.Item1.OperationId,
                model.Item1.RefArrete,
                model.Item1.DateOperation)
                              select new RapportViewModel() {
                                  Acte = r.ACTE_REF_ARR + "/" + CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(r.ACTE_ANN_VISA).ToString(),
                                  Action = GetStatusById(r.ACTE_STADE),
                                  User = GetUserLibByCode(r.CREATED_BY),
                                  ActionDate = r.CREATION_DATE.Value.ToShortDateString()
                              }).ToList();

            Tuple<
                CritereRapportViewModel,
                List<RapportViewModel>> tuple = new Tuple<
                    CritereRapportViewModel,
                    List<RapportViewModel>>(
                        model.Item1,
                        rapportList);

            return View(tuple);
        }

        [HttpPost]
        public ActionResult GenererFichierPlat(string[] selectedItems)
        {
            var str = "";

            foreach (var item in selectedItems)
            {
                if (item != "false")
                    str += item + Environment.NewLine;
            }

            return File(Encoding.UTF8.GetBytes(str),
                "text/plain",
                string.Format("{0}.txt", "GO_"+DateTime.Now));
        }

        public string GetStatusById(string id) {
            var stade = "";
            switch (id)
            {
                case "1":
                    stade = "Préparation";
                    break;
                case "2":
                    stade = "Contrôlé";
                    break;
                case "3":
                    stade = "Signé";
                    break;
                case "4":
                    stade = "Envoyé";
                    break;
                case "5":
                    stade = "Visé";
                    break;
                default:
                    stade = "Non défini";
                    break;
            }
            return stade;
        }

        public string GetUserLibByCode(short code)
        {
            var user = _userGOService.GetUserByCode(code);
            if (user == null) return "Indéfini";
            else return user.LIB_USR;
        }
    }
}
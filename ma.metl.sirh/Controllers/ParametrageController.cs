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
    public class ParametrageController : Controller
    {
        IParametrageQuotaService parametrageQuotaService;
        IGradeService gradeService;
        IParametrageClassementService parametrageClassementService;
        ICentreExamenService centreExamenService;
        IExamenService examenService;
        IDirectionService directionService;
        ISpecialiteService specialiteService;
        IMatiereService matiereService;
        IExamenCentreExamenService examenCentreExamenService;
        IMatiereExamenService matiereExamenService;
        IDetailAvancementService detailAvancementService;
        ILocaliteService localiteService;


        public ParametrageController(IParametrageQuotaService parametrageQuotaService, IGradeService gradeService, IParametrageClassementService parametrageClassementService, ICentreExamenService centreExamenService, IExamenService examenService, IDirectionService directionService, ISpecialiteService specialiteService, IMatiereService matiereService, IExamenCentreExamenService examenCentreExamenService, IMatiereExamenService matiereExamenService, ICommissionService commissionService, IDetailAvancementService detailAvancementService, ILocaliteService localiteService)
        {
            this.parametrageQuotaService = parametrageQuotaService;
            this.gradeService = gradeService;
            this.parametrageClassementService = parametrageClassementService;
            this.centreExamenService = centreExamenService;
            this.examenService = examenService;
            this.directionService = directionService;
            this.specialiteService = specialiteService;
            this.matiereService = matiereService;
            this.examenCentreExamenService = examenCentreExamenService;
            this.matiereExamenService = matiereExamenService;
            this.detailAvancementService = detailAvancementService;
            this.localiteService = localiteService;
        }
        //
        // GET: /Parametrage/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Parametragequota(int? Annee, int? Grades)
        {
            List<ParametrageQuota> listParam = new List<ParametrageQuota>();
            if (Annee == null && Grades == null)
            {
                listParam = parametrageQuotaService.GetAll().Where(x=>x.TypeFlux.Equals("AC")).ToList();
            }
            else
            {
                if (Annee != null)
                {
                    listParam = parametrageQuotaService.GetAll().Where(x => x.Annee.Equals(Annee.ToString()) && x.TypeFlux.Equals("AC")).ToList();
                    if (Grades != null)
                    {
                        listParam = listParam.Where(x => x.GradeAcces.Id == Grades).ToList();
                    }
                }
                if (Annee == null)
                {
                    if (Grades != null)
                    {
                        listParam = parametrageQuotaService.GetAll().Where(x => x.GradeAcces.Id == Grades && x.TypeFlux.Equals("AC")).ToList();
                    }
                }
            }
            List<String> listAnnee = new List<string>();
            // alimenter les listes 
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            ViewBag.Annee = new SelectList(listAnnee, Annee);
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description", Grades);

            ViewBag.AnneeA = new SelectList(listAnnee);
            ViewBag.GradeO = new SelectList(gradeService.GetAll(), "Id", "Description");
            ViewBag.GradeA = new SelectList(gradeService.GetAll(), "Id", "Description");

            ViewBag.AnneeM = new SelectList(listAnnee);
            ViewBag.GradeOM = new SelectList(gradeService.GetAll(), "Id", "Description");
            ViewBag.GradeAM = new SelectList(gradeService.GetAll(), "Id", "Description");

            Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota> tuple = new Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota>(listParam, new ParametrageQuota());

            return View(tuple);  
        }

        [HttpPost]
        public ActionResult CreerQuota(Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota> param)
        {
            ParametrageQuota parametre = new ParametrageQuota();
            parametre.GradeAcces = gradeService.GetById(Convert.ToInt32(param.Item2.GradeIdAcces));
            parametre.GradeOccupe = gradeService.GetById(Convert.ToInt32(param.Item2.GradeIdOccupe));
            parametre.NbrPoste = param.Item2.NbrPoste;
            parametre.Quota = param.Item2.Quota;
            parametre.Annee = param.Item2.Annee;
            parametre.Commentaire = param.Item2.Commentaire;
            parametre.TypeFlux = TypeFlux.AC.ToString();
            parametrageQuotaService.Create(parametre);
            
            return RedirectToAction("Parametragequota");
        }

        public ActionResult SupprimerQuota(string quotaId)
        {
            ParametrageQuota param = parametrageQuotaService.GetById(Convert.ToInt32(quotaId));
            parametrageQuotaService.Delete(param);

            return RedirectToAction("Parametragequota");
        }

        public ActionResult SupprimerQuotaE(string quotaId)
        {
            ParametrageQuota param = parametrageQuotaService.GetById(Convert.ToInt32(quotaId));
            parametrageQuotaService.Delete(param);

            return RedirectToAction("ParametragequotaE");
        }

        [HttpPost]
        public ActionResult ModifierQuota(Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota> param, string quotaIdM,string AnneeM, string GradeOM, string GradeAM)
        {
            ParametrageQuota parametre = parametrageQuotaService.GetById(Convert.ToInt32(quotaIdM));
            parametre.GradeAcces = gradeService.GetById(Convert.ToInt32(param.Item2.GradeIdAcces));
            parametre.GradeOccupe = gradeService.GetById(Convert.ToInt32(param.Item2.GradeIdOccupe));
            parametre.NbrPoste = param.Item2.NbrPoste;
            parametre.Anciennete = param.Item2.Anciennete;
            parametre.Quota = param.Item2.Quota;
            parametre.Annee = param.Item2.Annee;
            parametre.Commentaire = param.Item2.Commentaire;
            parametrageQuotaService.Update(parametre);
            return RedirectToAction("Parametragequota");

        }

        [HttpPost]
        public ActionResult ModifierQuotaE(Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota> param, string quotaIdM)
        {
            ParametrageQuota parametre = parametrageQuotaService.GetById(Convert.ToInt32(quotaIdM));
            parametre.GradeAcces = gradeService.GetById(Convert.ToInt32(param.Item2.GradeIdAcces));
            parametre.GradeOccupe = gradeService.GetById(Convert.ToInt32(param.Item2.GradeIdOccupe));
            parametre.NbrPoste = param.Item2.NbrPoste;
            parametre.Anciennete = param.Item2.Anciennete;
            parametre.Quota = param.Item2.Quota;
            parametre.Annee = param.Item2.Annee;
            parametre.Commentaire = param.Item2.Commentaire;
            parametrageQuotaService.Update(parametre);
            return RedirectToAction("ParametragequotaE");

        }

        public ActionResult Parametrageclassement( int? Annee, int? Grade)
        {

            List<ParametrageClassement> listParam=new List<ParametrageClassement>();

            if (Annee == null && Grade == null)
            {
                listParam = parametrageClassementService.GetAll().Where(x => x.TypeFlux == "AC").ToList();
            }
            else
            {
                listParam = parametrageClassementService.GetAll().Where(x => x.TypeFlux == "AC").ToList();
                if (Annee != null)
                {
                    listParam = listParam.Where(x => x.Annee.Equals(Annee.ToString()) && x.TypeFlux == "AC").ToList();
                    if (Grade != null)
                    {
                        listParam = listParam.Where(x => x.Grade.Id == Grade).ToList();
                    }
                }
                if (Annee == null)
                {

                    if (Grade != null)
                    {
                        listParam = listParam.Where(x => x.Grade.Id == Grade && x.TypeFlux == "AC").ToList();
                    }
                }
            }
            List<String> listAnnee = new List<string>();
            // alimenter les listes 
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            ViewBag.Annee = new SelectList(listAnnee, Annee);
            ViewBag.Grade = new SelectList(gradeService.GetAll(), "Id", "Description", Grade);

            ViewBag.AnneeM = new SelectList(listAnnee);
            ViewBag.GradesM = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");

            Tuple<IEnumerable<ParametrageClassement>, ParametrageClassement> tuple = new Tuple<IEnumerable<ParametrageClassement>, ParametrageClassement>(listParam, new ParametrageClassement());

            return View(tuple); 
        }

        [HttpPost]
        public ActionResult CreerCritere(Tuple<IEnumerable<ParametrageClassement>, ParametrageClassement> param)
        {
                ParametrageClassement parametre = new ParametrageClassement();
                parametre.Critere = param.Item2.Critere;
                parametre.Valeur = param.Item2.Valeur;
                parametre.Annee = param.Item2.Annee;
                parametre.Grade = gradeService.GetById(Convert.ToInt32(param.Item2.GradeId));
                parametre.TypeFlux = TypeFlux.AC.ToString();
                parametrageClassementService.Create(parametre);
                return RedirectToAction("Parametrageclassement");
        }

        
        public ActionResult SupprimerCritere(string critereId)
        {
            ParametrageClassement param = parametrageClassementService.GetById(Convert.ToInt32(critereId));
            parametrageClassementService.Delete(param);

            return RedirectToAction("Parametrageclassement");
        }

        [HttpPost]
        public ActionResult ModifierCritere(Tuple<IEnumerable<ParametrageClassement>, ParametrageClassement> param,string critereIdM, string AnneeM, int GradesM)
        {
            ParametrageClassement parametre = parametrageClassementService.GetById(Convert.ToInt32(critereIdM));
            parametre.Critere = param.Item2.Critere;
            parametre.Valeur = param.Item2.Valeur;
            parametre.Annee = AnneeM;
            parametre.Grade = gradeService.GetById(Convert.ToInt32(GradesM));
            parametrageClassementService.Update(parametre);
            return RedirectToAction("Parametrageclassement");

        }

        public ActionResult ParametragequotaE(int? Annee, int? Grades)
        {
            List<ParametrageQuota> listParam = new List<ParametrageQuota>();
            if (Annee == null && Grades == null)
            {
                listParam = parametrageQuotaService.GetAll().Where(x => x.TypeFlux.Equals("AE")).ToList();
            }
            else
            {
                if (Annee != null)
                {
                    listParam = parametrageQuotaService.GetAll().Where(x => x.Annee.Equals(Annee.ToString()) && x.TypeFlux.Equals("AE")).ToList();
                    if (Grades != null)
                    {
                        listParam = listParam.Where(x => x.GradeAcces.Id == Grades).ToList();
                    }
                }
                if (Annee == null)
                {
                    if (Grades != null)
                    {
                        listParam = parametrageQuotaService.GetAll().Where(x => x.GradeAcces.Id == Grades && x.TypeFlux.Equals("AE")).ToList();
                    }
                }
            }
            List<String> listAnnee = new List<string>();
            // alimenter les listes 
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            ViewBag.Annee = new SelectList(listAnnee, Annee);
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description", Grades);

            ViewBag.AnneeA = new SelectList(listAnnee);
            ViewBag.GradeO = new SelectList(gradeService.GetAll(), "Id", "Description");
            ViewBag.GradeA = new SelectList(gradeService.GetAll(), "Id", "Description");

            ViewBag.AnneeM = new SelectList(listAnnee);
            ViewBag.GradeOM = new SelectList(gradeService.GetAll(), "Id", "Description");
            ViewBag.GradeAM = new SelectList(gradeService.GetAll(), "Id", "Description");

            Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota> tuple = new Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota>(listParam, new ParametrageQuota());

            return View(tuple);
        }

        [HttpPost]
        public ActionResult CreerQuotaE(Tuple<IEnumerable<ParametrageQuota>, ParametrageQuota> param)
        {
            ParametrageQuota parametre = new ParametrageQuota();
            parametre.GradeAcces = gradeService.GetById(Convert.ToInt32(param.Item2.GradeIdAcces));
            parametre.GradeOccupe = gradeService.GetById(Convert.ToInt32(param.Item2.GradeIdOccupe));
            parametre.NbrPoste = param.Item2.NbrPoste;
            parametre.Anciennete = param.Item2.Anciennete;
            parametre.Quota = param.Item2.Quota;
            parametre.Annee = param.Item2.Annee;
            parametre.TypeFlux = TypeFlux.AE.ToString();
            parametrageQuotaService.Create(parametre);

            return RedirectToAction("ParametragequotaE");
        }

        public ActionResult ParametrageclassementE(int? Annee, int? Grades)
        {
            List<ParametrageClassement> listParam = new List<ParametrageClassement>();
            if (Annee == null && Grades == null)
            {
                listParam = parametrageClassementService.GetAll().Where(x=>x.TypeFlux=="AE").ToList();
            }
            else
            {
                if (Annee != null)
                {
                    listParam = parametrageClassementService.GetAll().Where(x => x.Annee.Equals(Annee.ToString()) && x.TypeFlux == "AE").ToList();
                    if (Grades != null)
                    {
                        listParam = listParam.Where(x => x.Grade.Id == Grades).ToList();
                    }
                }
                if (Annee == null)
                {
                    if (Grades != null)
                    {
                        listParam = parametrageClassementService.GetAll().Where(x => x.Grade.Id == Grades && x.TypeFlux == "AE").ToList();
                    }
                }
            }
            List<String> listAnnee = new List<string>();
            // alimenter les listes 
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            ViewBag.Annee = new SelectList(listAnnee, Annee);
            ViewBag.Grade = new SelectList(gradeService.GetAll(), "Id", "Description", Grades);

            ViewBag.AnneeM = new SelectList(listAnnee);
            ViewBag.GradesM = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");

            Tuple<IEnumerable<ParametrageClassement>, ParametrageClassement> tuple = new Tuple<IEnumerable<ParametrageClassement>, ParametrageClassement>(listParam, new ParametrageClassement());

            return View(tuple);
        }

        [HttpPost]
        public ActionResult CreerCritereE(Tuple<IEnumerable<ParametrageClassement>, ParametrageClassement> param, string Annee, string Grade)
        {
            ParametrageClassement parametre = new ParametrageClassement();
            parametre.Critere = param.Item2.Critere;
            parametre.Valeur = param.Item2.Valeur;
            parametre.Annee = Annee;
            parametre.Grade = gradeService.GetById(Convert.ToInt32(Grade));
            parametre.TypeFlux = TypeFlux.AE.ToString();
            parametrageClassementService.Create(parametre);
            return RedirectToAction("ParametrageclassementE");
        }

        public ActionResult CentresExamen()
        {
            List<CentreExamen> listParam = new List<CentreExamen>();
            ViewBag.localite = new SelectList(localiteService.GetAll(), "Id", "Intitule");
            ViewBag.localiteM = new SelectList(localiteService.GetAll(), "Id", "Intitule");
            listParam = centreExamenService.GetAll().ToList();
            Tuple<IEnumerable<CentreExamen>, CentreExamen> tuple = new Tuple<IEnumerable<CentreExamen>, CentreExamen>(listParam, new CentreExamen());
            
            return View(tuple); 
        }

        public ActionResult ParametrageExamen()
        {
            List<Examen> listParam = new List<Examen>();
            listParam = examenService.GetAll().ToList();
            Tuple<IEnumerable<Examen>, Examen> tuple = new Tuple<IEnumerable<Examen>, Examen>(listParam, new Examen());
            List<String> listAnnee = new List<string>();
            // alimenter les listes 
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            ViewBag.Annee = new SelectList(listAnnee);
            ViewBag.Grade = new SelectList(gradeService.GetAll(), "Id", "Description");
            ViewBag.Directions = new SelectList(directionService.GetAll(), "Id", "Description");
            ViewBag.AnneeA = new SelectList(listAnnee);
            ViewBag.GradeA = new SelectList(gradeService.GetAll(), "Id", "Description");
            ViewBag.DirectionsM = new SelectList(directionService.GetAll(), "Id", "Description");
            ViewBag.AnneeM = new SelectList(listAnnee);
            ViewBag.GradeM = new SelectList(gradeService.GetAll(), "Id", "Description");
            ViewBag.Centres = new MultiSelectList(centreExamenService.GetAll(), "Id", "name");
            ViewBag.Matieres = new SelectList(matiereService.GetAll(), "Id", "Intitule");
            return View(tuple); 
        }

        [HttpPost]
        public ActionResult ParametrageExamen(Tuple<IEnumerable<Examen>, Examen> param, string Annee, int? Grade, string Datelimite)
        {
            List<Examen> listParam = new List<Examen>();

            if (Annee == "Sélectionnez" && Grade == 0 && Datelimite == "")
            {
                listParam = examenService.GetAll().ToList();
            }
            else
            {
                if (Annee != null && Annee != "Sélectionnez")
                {
                    listParam = examenService.GetAll().Where(x => x.Annee.Equals(Annee.ToString())).ToList();
                    if (Grade != null)
                    {
                        listParam = listParam.Where(x => x.Grade.Id == Grade).ToList();
                    }
                    if (Datelimite != null && Datelimite!= "")
                    {
                        listParam = listParam.Where(x => x.Datelimite == Convert.ToDateTime(Datelimite)).ToList();
                    }
                }
                if (Annee == "Sélectionnez")
                {
                    listParam = examenService.GetAll().ToList();
                    if (Grade != 0)
                    {
                        listParam = examenService.GetAll().Where(x => x.Grade.Id == Grade).ToList();

                        if (Datelimite != "")
                        {
                            listParam = listParam.Where(x => x.Datelimite == Convert.ToDateTime(Datelimite)).ToList();
                        }
                    }
                    else if (Datelimite != null)
                    {
                        listParam = examenService.GetAll().Where(x => x.Datelimite == Convert.ToDateTime(Datelimite)).ToList();
                    }
                }
            }

            Tuple<IEnumerable<Examen>, Examen> tuple = new Tuple<IEnumerable<Examen>, Examen>(listParam, new Examen());
            List<String> listAnnee = new List<string>();
            // alimenter les listes 
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            ViewBag.Annee = new SelectList(listAnnee, Annee);
            ViewBag.Grade = new SelectList(gradeService.GetAll(), "Id", "Description", Grade);
            ViewBag.Directions = new SelectList(directionService.GetAll(), "Id", "Description");
        
          
          
            ViewBag.AnneeA = new SelectList(listAnnee);
            ViewBag.GradeA = new SelectList(gradeService.GetAll(), "Id", "Description");
            ViewBag.DirectionsM = new SelectList(directionService.GetAll(), "Id", "Description");
            ViewBag.AnneeM = new SelectList(listAnnee);
            ViewBag.GradeM = new SelectList(gradeService.GetAll(), "Id", "Description");
            ViewBag.Centres = new MultiSelectList(centreExamenService.GetAll(), "Id", "name");
            ViewBag.Matieres = new SelectList(matiereService.GetAll(), "Id", "Intitule");
            return View(tuple);
        }

        //[HttpPost]
        public ActionResult CreerExamen(Tuple<IEnumerable<Examen>, Examen> param, string AnneeA, string GradeA, string Directions, int[] Centres, string[] AliasMatiere, string[] AliasCoefficient)
        {
            Examen examen = new Examen();
            examen.Intitule = param.Item2.Intitule;
            examen.Datelimite = Convert.ToDateTime(param.Item2.Datelimite);
            examen.DateExamen = Convert.ToDateTime(param.Item2.DateExamen);
            examen.Annee = AnneeA;
            examen.MoyennePassage = param.Item2.MoyennePassage;
            examen.NoteEliminatoire = param.Item2.NoteEliminatoire;
            examen.Direction = directionService.GetById(Convert.ToInt32(Directions));
            examen.Grade = gradeService.GetById(Convert.ToInt32(GradeA));
            examenService.Create(examen);
            long id = examen.Id; 
            //creer la liaison examen centre examen 
            for (int i = 0; i < Centres.Count(); i++)
            {
                ExamenCentreExamen examCentre = new ExamenCentreExamen();
                examCentre.Examen_Id = id;
                examCentre.CentreExamen_Id = Centres[i];
                examenCentreExamenService.Create(examCentre);
            }
            

            //ajout des matières 
            
            for(int i= 0; i<AliasCoefficient.Length; i++)
            {
                MatiereExamen matiereExamen = new MatiereExamen();
                matiereExamen.Examen_Id = id;
                Matiere matiere = matiereService.GetByLibelle(AliasMatiere[i]);
                matiereExamen.Matiere_Id = matiere.Id; 
                matiereExamen.Coefficient = Convert.ToInt32(AliasCoefficient[i]);
                matiereExamenService.Create(matiereExamen);
            }

            return RedirectToAction("ParametrageExamen"); 
        }

        public ActionResult ModifierExamen(Tuple<IEnumerable<Examen>, Examen> param, string examenIdM, string AnneeM, string GradeM, string DirectionsM, int[] CentresM, string[] AliasMatiereM, string[] AliasCoefficientM)
        {
            int idExamen = Convert.ToInt32(examenIdM);
            Examen examen = examenService.GetById(idExamen);
            examen.Intitule = param.Item2.Intitule;
            examen.Datelimite = Convert.ToDateTime(param.Item2.Datelimite);
            examen.DateExamen = Convert.ToDateTime(param.Item2.DateExamen);
            examen.Annee = AnneeM;
            examen.MoyennePassage = param.Item2.MoyennePassage;
            examen.NoteEliminatoire = param.Item2.NoteEliminatoire;
            examen.Direction = directionService.GetById(Convert.ToInt32(DirectionsM));
            examen.Grade = gradeService.GetById(Convert.ToInt32(GradeM));

            List<ExamenCentreExamen> centres = examenCentreExamenService.GetAll().Where(x => x.Examen_Id == idExamen).ToList();
            foreach (ExamenCentreExamen c in centres)
            {
                examenCentreExamenService.Delete(c);
            }
            List<MatiereExamen> matieres = matiereExamenService.GetAll().Where(x => x.Examen_Id == idExamen).ToList();
            foreach (MatiereExamen m in matieres)
            {
                matiereExamenService.Delete(m);
            }
            //creer la liaison examen centre examen 
            for (int i = 0; i < CentresM.Count(); i++)
            {
                ExamenCentreExamen examCentre = new ExamenCentreExamen();
                examCentre.Examen_Id = idExamen;
                examCentre.CentreExamen_Id = CentresM[i];
                examenCentreExamenService.Create(examCentre);
            }


            //ajout des matières 

            for (int i = 0; i < AliasCoefficientM.Length; i++)
            {
                MatiereExamen matiereExamen = new MatiereExamen();
                matiereExamen.Examen_Id = idExamen;
                Matiere matiere = matiereService.GetByLibelle(AliasMatiereM[i]);
                matiereExamen.Matiere_Id = matiere.Id;
                matiereExamen.Coefficient = Convert.ToInt32(AliasCoefficientM[i]);
                matiereExamenService.Create(matiereExamen);
            }

            return RedirectToAction("ParametrageExamen");
        }

        public ActionResult SupprimerExamen(string examenId)
        {
            Examen examen = examenService.GetById(Convert.ToInt32(examenId));
            examenService.Delete(examen);
            return RedirectToAction("ParametrageExamen"); 

        }

        [HttpPost]
        public ActionResult CreerCentreExamen(Tuple<IEnumerable<CentreExamen>, CentreExamen> param, string localite)
        {
            CentreExamen parametre = new CentreExamen();
            parametre.name = param.Item2.name;
            parametre.adresse = param.Item2.adresse;
            parametre.Localite = localiteService.GetById(Convert.ToInt32(localite)); 
            centreExamenService.Create(parametre);
            return RedirectToAction("CentresExamen");
        }

        [HttpPost]
        public ActionResult ModifierCentre(Tuple<IEnumerable<CentreExamen>, CentreExamen> param, string centreIdM, string localiteM)
        {
            CentreExamen parametre = centreExamenService.GetById(Convert.ToInt32(centreIdM));
            parametre.name = param.Item2.name;
            parametre.adresse = param.Item2.adresse;
            parametre.Localite = localiteService.GetById(Convert.ToInt32(localiteM));
            centreExamenService.Update(parametre);
            return RedirectToAction("CentresExamen");

        }

        public ActionResult SupprimerCentre(string centreId)
        {
            CentreExamen param = centreExamenService.GetById(Convert.ToInt32(centreId));
            centreExamenService.Delete(param);

            return RedirectToAction("CentresExamen");
        }

        public JsonResult getCentresExamen(string examenId)
        {
            try
            {

                List<CentreExamen> listcentres = centreExamenService.getListCentresByExamen(Convert.ToInt32(examenId)); 

                var centres = new List<object>();

                foreach(CentreExamen c in listcentres)
                {
                    centres.Add(new { Id = c.Id, Name = c.name }); 
                }
                return Json(centres, JsonRequestBehavior.AllowGet);
            }catch(Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }

        public JsonResult getExamenById(string examenId)
        {
            try
            {
                Examen exam = examenService.GetById(Convert.ToInt32(examenId));

                var examen = new { dateLimite = exam.Datelimite, dateExamen = exam.DateExamen, intitule= exam.Intitule, moyennePassage= exam.MoyennePassage, noteEliminatoire=exam.NoteEliminatoire, grade = exam.Grade.Id, direction = exam.Direction.Id, annee = exam.Annee};

                return Json(examen, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }


        public JsonResult getCentreById(string centreId)
        {
            try
            {
                CentreExamen centre = centreExamenService.GetById(Convert.ToInt32(centreId));
                return Json(centre, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }

        public JsonResult getCritereById(string critereId)
        {
            try
            {

                ParametrageClassement parametre = parametrageClassementService.GetById(Convert.ToInt32(critereId));

                return Json(new { annee = parametre.Annee, valeur =parametre.Valeur, grade = parametre.Grade.Id, critere= parametre.Critere}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }


        public JsonResult getMatieres(string examenId)
        {
            try
            {
                
                Examen exam = examenService.GetById(Convert.ToInt32(examenId));
                long i = exam.Id;
                List<MatiereExamen> listmatiere = matiereService.getListMatiereByExamen(exam.Id); 

                var matieres = new List<object>();

                foreach (MatiereExamen m in listmatiere)
                {
                    Matiere matiere = matiereService.GetById(m.Matiere_Id);
                    matieres.Add(new { intitule = matiere.Intitule, coefficient = m.Coefficient });
                }
                return Json(matieres, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }


        // Paramétrage des spécialités  

        public ActionResult ParametrageSpecialite(int? Examen, int? Grade)
        {
            List<Specialite> listParam = specialiteService.GetAll().ToList();

                if (Examen != null)
                {
                    listParam = listParam.Where(x => x.ExamenId == Examen).ToList();
                    if (Grade != null)
                    {
                        listParam = listParam.Where(x => x.Grade.Id == Grade).ToList();
                    }
                }
                if (Examen == null)
                {
                    if (Grade != null)
                    {
                        listParam = listParam.Where(x => x.Grade.Id == Grade).ToList();
                    }
                }
            

            ViewBag.Examen = new SelectList(examenService.GetAll(), "Id", "Intitule", Examen);
            ViewBag.Grade = new SelectList(gradeService.GetAll(), "Id", "Description", Grade);

            ViewBag.ExamenM = new SelectList(examenService.GetAll(), "Id", "Intitule");
            ViewBag.GradeM = new SelectList(gradeService.GetAll(), "Id", "Description");

            Tuple<IEnumerable<Specialite>, Specialite> tuple = new Tuple<IEnumerable<Specialite>, Specialite>(listParam, new Specialite());

            return View(tuple);
        }

         [HttpPost]
        public ActionResult CreerSpecialite(Tuple<IEnumerable<Specialite>, Specialite> param, string Examen, string Grade)
        {
                Specialite parametre = new Specialite();
                parametre.Intitule = param.Item2.Intitule;
                parametre.Grade = gradeService.GetById(Convert.ToInt32(Grade));
                parametre.Examen = examenService.GetById(Convert.ToInt32(Examen));
                specialiteService.Create(parametre);
                return RedirectToAction("ParametrageSpecialite");
        }

        
        public ActionResult SupprimerSpecialite(string specialiteId)
        {
            Specialite param = specialiteService.GetById(Convert.ToInt32(specialiteId));
            specialiteService.Delete(param);

            return RedirectToAction("ParametrageSpecialite");
        }

        [HttpPost]
        public ActionResult ModifierSpecialite(Tuple<IEnumerable<Specialite>, Specialite> param, string specialiteId, string ExamenM, int GradeM)
        {
            Specialite parametre = specialiteService.GetById(Convert.ToInt32(specialiteId));
            parametre.Intitule = param.Item2.Intitule;
            parametre.Grade = gradeService.GetById(Convert.ToInt32(GradeM));
            parametre.Examen = examenService.GetById(Convert.ToInt32(ExamenM));
            specialiteService.Update(parametre);
            return RedirectToAction("ParametrageSpecialite");
        }

        // Paramétrage des matières  

        public ActionResult ParametrageMatieres()
        {
            List<Matiere> listParam = new List<Matiere>();
            listParam = matiereService.GetAll().ToList();
            Tuple<IEnumerable<Matiere>, Matiere> tuple = new Tuple<IEnumerable<Matiere>, Matiere>(listParam, new Matiere());

            return View(tuple);
        }

        [HttpPost]
        public ActionResult CreerMatiere(Tuple<IEnumerable<Matiere>, Matiere> param)
        {
            Matiere parametre = new Matiere();
            parametre.Intitule = param.Item2.Intitule;
            matiereService.Create(parametre);
            return RedirectToAction("ParametrageMatieres");
        }


        public ActionResult SupprimerMatiere(string matiereId)
        {
            Matiere param = matiereService.GetById(Convert.ToInt32(matiereId));
            matiereService.Delete(param);

            return RedirectToAction("ParametrageMatieres");
        }

        [HttpPost]
        public ActionResult ModifierMatiere(Tuple<IEnumerable<Matiere>, Matiere> param, string MatiereIdM)
        {
            Matiere parametre = matiereService.GetById(Convert.ToInt32(MatiereIdM));
            parametre.Intitule = param.Item2.Intitule;
            matiereService.Update(parametre);
            return RedirectToAction("ParametrageMatieres");
        }


        //retourne le nombre total des promouvables pour l'avancement au choix 

        public JsonResult getNombreTotalPromouvableAC(string grade , string annee)
        {
            try
            {
                int nbrPromouvable = detailAvancementService.GetNombreTotalPromouvableAC(Convert.ToInt32(grade), annee);
                return Json(nbrPromouvable, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }

        //retourne le nombre total des promouvables pour l'avancement sur examen 

        public JsonResult getNombreTotalPromouvableAE(string grade, string annee)
        {
            try
            {
                decimal nbrPoste = 0;
                int nbrPromouvable = detailAvancementService.GetNombreTotalPromouvableAE(Convert.ToInt32(grade), annee);
                ParametrageQuota p = parametrageQuotaService.GetNbrPoste(annee, grade);
                if (p != null)
                {
                    nbrPoste= p.NbrPoste;
                }

                var result = new { NbrPromouvable = nbrPromouvable, NbrPoste = nbrPoste, error="" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var result = new { error = "exception occured while processing your request" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getNombrePosteAC(string grade, string annee)
        {
            try
            {
                decimal nbrPoste = 0;
                QuotaDto p = parametrageQuotaService.GetNbrPosteAC(annee, Convert.ToInt32(grade));
                if (p != null)
                {
                    nbrPoste = p.NbrPosteOuvrir;
                }

                return Json(nbrPoste, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }

        public JsonResult getQuotaById(string quotaId)
        {
            try
            {
                ParametrageQuota param = parametrageQuotaService.GetById(Convert.ToInt32(quotaId));

                return Json(param, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }
	}
}
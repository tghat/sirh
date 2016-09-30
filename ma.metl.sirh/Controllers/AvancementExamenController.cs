using ma.metl.sirh.Common.TraceHistory;
using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Service;
using Microsoft.International.Formatters;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Odbc;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ma.metl.sirh.Controllers
{
    public class AvancementExamenController : BaseController
    {

        IGradeService gradeService;
        IDirectionService directionService;
        ICommissionService commissionService;
        ILocaliteService localiteService;
        IExamenService examenService;
        ICandidatService candidatService;
        IMatiereExamenService matiereExamenService;
        INotationService notationService;
        ISpecialiteService specialiteService;
        ICandidatureService candidatureService;
        IDetailAvancementService detailAvancementService;
        ICandidatGOService canidatGOService;
        IParametrageQuotaService parametrageQuotaService;
        IHistoriqueService historiqueService;
        IUsersService userService;



        public AvancementExamenController(ICommissionService commissionService, IGradeService gradeService, IDirectionService directionService, ILocaliteService localiteService, IExamenService examenService, ICandidatService candidatService, IMatiereExamenService matiereExamenService, ISpecialiteService specialiteService, ICandidatureService candidatureService, ICandidatGOService canidatGOService, INotationService notationService, IParametrageQuotaService parametrageQuotaService, IHistoriqueService historiqueService, IDetailAvancementService detailAvancementService, IUsersService userService)
            :base(historiqueService, detailAvancementService)

        {
            this.commissionService = commissionService;
            this.gradeService = gradeService;
            this.directionService = directionService;
            this.localiteService = localiteService;
            this.examenService = examenService;
            this.candidatService = candidatService;
            this.matiereExamenService = matiereExamenService;
            this.notationService = notationService; 
            this.specialiteService = specialiteService;
            this.candidatureService = candidatureService;
            this.canidatGOService = canidatGOService;
            this.parametrageQuotaService = parametrageQuotaService;
            this.userService = userService;
            this.historiqueService = historiqueService;
            this.detailAvancementService = detailAvancementService;
        }
        //
        // GET: /AvancementExamen/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AvExamenGestionCandidats(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> dto, string candidatId, string detail, string name)
        {
            //récupération de la liste des candidats par critères de recherche
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();
            CandidatResultatDto candidatSynthese = new CandidatResultatDto();
            string source = (string)Session["source"];
            if (source != null)
            {
                dto = (Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto>)Session["dto"];
                Session["source"] = null;
            }
            else
            {
                Session["dto"] = dto;
            }

            if (dto.Item2.GradeId == 0 && dto.Item2.RegionId == 0 && dto.Item2.CommissionId == 0 && dto.Item2.DirectionId == 0 && dto.Item2.AncienneteEchelon == 0 && dto.Item2.AncienneteGrade == 0
                    && dto.Item2.NumDoti == null && dto.Item2.DateNaissance == null && dto.Item2.DateRecrutement == null && dto.Item2.AnneeProm == null)
            {
                candidats = new List<CandidatResultatDto>();
                candidatId = null;
            }

            else
            {
                candidats = candidatService.GetAllCandidatExamen(dto.Item2);
            }

            //Syntese cnadidat
            ViewBag.rendredSyntese = false;

            if (candidatId != null && candidatId != "" && name.Equals("syntese"))
            {
                int id = Int32.Parse(candidatId);
                string detailAvancement = detail;
                Notation notation = notationService.GetByNotationByIdDetail(Convert.ToInt32(detailAvancement));
                if (notation != null)
                {
                    candidatSynthese.NoteEcrite = notation.NoteEcrite;
                    candidatSynthese.NoteConnaissSp = notation.NoteConnaissSp;
                    candidatSynthese.NoteConnaissMin = notation.NoteConnaissMin;
                    candidatSynthese.NoteOrale = notation.NoteOrale;
                    candidatSynthese.NotePresConnaissanceG = notation.NotePresConnaissanceG;
                    candidatSynthese.NoteGlobale = notation.NoteGlobale;
                }
                candidatSynthese = candidatService.GetSyntheseCandidat(id);
                ViewBag.rendredSyntese = true;
                ViewBag.detailAvancementId = detailAvancement;
                candidats = candidatService.GetCandidatExamenById(id);
                candidatSynthese.Historiques = historiqueService.GetByIdDetailAvancement(Convert.ToInt32(detailAvancement));
            }


            string errorMessage = TempData["ErrorMessage"] as string;
            string successMessage = TempData["successMessage"] as string;

            if (!String.IsNullOrEmpty(errorMessage) || !String.IsNullOrEmpty(successMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
                ViewBag.successMessage = successMessage;
                //candidats = candidatService.GetAllCandidatSurExamen();
            }


            //Alimentation des listes de choix 
            List<String> listAnnee = new List<string>();
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }

            //Chargement des listes de choix
            ViewBag.Regions = new SelectList(localiteService.GetAll(), "Id", "Intitule", dto.Item2.RegionId);
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x=>x.Description), "Id", "Description", dto.Item2.GradeId);
            ViewBag.Annee = new SelectList(listAnnee, dto.Item2.AnneeProm);
            ViewBag.Directions = new SelectList(directionService.GetAll(), "Id", "Description", dto.Item2.DirectionId);
            ViewBag.Commissions = new SelectList(commissionService.GetAll(), "Id", "Titre", dto.Item2.CommissionId);

            //Return value
            Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> tuple = new Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto>(candidats, dto.Item2, candidatSynthese, new CandidatDto());
            return View(tuple);  



        }

        public ActionResult AvExamenGestionExamens(Tuple<CritereRechercheDto, IEnumerable<CandidatResultatDto>, IEnumerable<MatiereDto>, Email> param)
        {

            List<Examen> listExamens = new List<Examen>();
            List<CandidatResultatDto> listCandidat = new List<CandidatResultatDto>();
            List<MatiereExamen> listMatiere = new List<MatiereExamen>();
            CritereRechercheDto criteres = new CritereRechercheDto();
            List<MatiereDto> listeMatiereDto = new List<MatiereDto>();


            ////alimenter les listes 
            List<String> listAnnee = new List<string>();

            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();

            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            if (param.Item1.AnneeProm != null && param.Item1.GradeId != 0)
            {
                int idGrade = Convert.ToInt32(param.Item1.GradeId);
                listExamens = examenService.GetAll().Where(x => x.Annee == param.Item1.AnneeProm && x.Grade.Id == Convert.ToInt32(param.Item1.GradeId)).ToList();
            }
            //ViewBag.Annee = new SelectList(listAnnee, param.Item1.AnneeProm);
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description", param.Item1.GradeId);
            ViewBag.Examens = new SelectList(listExamens, "Id", "Intitule", param.Item1.ExamenId);
            ViewBag.Annee = new SelectList(listAnnee, param.Item1.AnneeProm);
            ViewBag.Directions = new SelectList(directionService.GetAll(), "Id", "Description");
            ViewBag.show = false;

            if (param.Item1.AnneeProm != null && param.Item1.ExamenId != 0 && param.Item1.ExamenId != 0 && param.Item1.DateExamen != null)
            {
                // permet d'avoir l nombre de candidat éligible à la date d'examen
                int NbrCandidatEligibleDateExamen = 0;
                int NbrCandidatEligibleFinAnnee = 0;
                listCandidat = candidatService.GetAllCandidatSurExamen(param.Item1.AnneeProm, param.Item1.GradeId);
                if (param.Item1.GradeId == 66 || param.Item1.GradeId == 104 || param.Item1.GradeId == 108)
                {

                    foreach (CandidatResultatDto c in listCandidat)
                    {
                        //c.AncienneteGrade = c.AncienneteGrade.AddYears(4);
                        c.DateEffet = c.DateEffet.Value.AddYears(4);
                    }
                }
                else
                {

                    foreach (CandidatResultatDto c in listCandidat)
                    {
                        //c.AncienneteGrade = c.AncienneteGrade.AddYears(6);
                        c.DateEffet = c.DateEffet.Value.AddYears(6);
                    }
                }
                foreach (CandidatResultatDto c in listCandidat)
                {
                    //if (c.AncienneteGrade <= Convert.ToDateTime(param.Item1.DateExamen))
                    //{
                    //    NbrCandidatEligibleDateExamen++;
                    //}
                    if (c.DateEffet <= Convert.ToDateTime(param.Item1.DateExamen))
                    {
                        NbrCandidatEligibleDateExamen++;
                    }
                }

                foreach (CandidatResultatDto c in listCandidat)
                {
                    DateTime date = new DateTime(DateTime.Now.Year, 12, 31);
                    //if (c.AncienneteGrade <= Convert.ToDateTime(date))
                    //{
                    //    NbrCandidatEligibleFinAnnee++;
                    //}
                    if (c.DateEffet <= Convert.ToDateTime(date))
                    {
                        NbrCandidatEligibleFinAnnee++;
                    }
                }
                //remplir la liste des heures 
                List<string> listH = new List<string>();
                var valuesH = Enum.GetValues(typeof(Heure)).Cast<Heure>();

                foreach (var r in valuesH)
                {
                    var field = r.GetType().GetField(r.ToString());
                    var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    listH.Add(attributes[0].Description.ToString());

                }

                //remplir la liste des minutes  
                List<string> listM = new List<string>();
                var valuesM = Enum.GetValues(typeof(Minute)).Cast<Minute>();

                foreach (var r in valuesM)
                {
                    var field = r.GetType().GetField(r.ToString());
                    var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    listM.Add(attributes[0].Description.ToString());

                }

                NbrCandidatEligibleFinAnnee = 600;
                NbrCandidatEligibleDateExamen = 200;


                if (listCandidat.Count != 0)
                {
                    ViewBag.nbrCandidatEligibleDateExamen = NbrCandidatEligibleDateExamen.ToString();
                    ViewBag.pourcentageCandidatDate = NbrCandidatEligibleDateExamen * 100 / NbrCandidatEligibleFinAnnee + "%";
                    ViewBag.pourcentageCandidatAnnee = NbrCandidatEligibleFinAnnee;
                    ViewBag.dateexamen = "Pourcentage de candidats éligible à la date du " + param.Item1.DateExamen + ":";
                    ViewBag.nbdateexamen = "Nombre de candidats éligible au " + param.Item1.DateExamen + ":";
                    ViewBag.nbrTotal = listCandidat.Count;

                    foreach (Examen e in listExamens)
                    {
                        if (e.Id == (long)param.Item1.ExamenId)
                        {
                            listMatiere = e.ListMatiereExamen;
                            ViewBag.IdExam = e.Id; 
                        }
                    }
                    foreach (MatiereExamen m in listMatiere)
                    {
                        MatiereDto matiere = new MatiereDto();
                        matiere.IdMatiere = m.Matiere_Id;
                        matiere.IdExamen = m.Examen_Id;
                        matiere.Intitule = m.Matiere.Intitule;
                        matiere.Date = m.DateMatiere;
                        matiere.Coefficient = m.Coefficient;
                        matiere.DureeH = m.DureeH;
                        matiere.DureeM = m.DureeM;
                        matiere.HeureDebutH = m.HeureDebutH;
                        matiere.HeureDebutM = m.HeureDebutM;
                        matiere.ListDureeH = new SelectList(listH, matiere.DureeH);
                        matiere.ListDureeM = new SelectList(listM, matiere.DureeM);
                        matiere.ListHeureDebutH = new SelectList(listH, matiere.HeureDebutH);
                        matiere.ListHeureDebutM = new SelectList(listM, matiere.HeureDebutM);
                        listeMatiereDto.Add(matiere);
                    }
                    ViewBag.show = true;
                }
                else
                {
                    ModelState.AddModelError("msgError", "Aucun candidat ne correspond aux critères de recherches spécifiés!");
                    ViewBag.show = false;
                }
                
               
            }
            param = new Tuple<CritereRechercheDto, IEnumerable<CandidatResultatDto>, IEnumerable<MatiereDto>, Email>(param.Item1, listCandidat, listeMatiereDto, new Email());
            return View(param); 
        }

        public JsonResult ValiderMatiere(string MatiereId, string ExamenId, DateTime DateMatiere, string DureeH, string DureeM, string HeureDebutH, string HeureDebutM)
        {
            string msg = "";
            try
            {
                MatiereExamen matiere = this.matiereExamenService.GetByIdMatiereIdExamen(Convert.ToInt64(MatiereId), Convert.ToInt64(ExamenId));
                matiere.DateMatiere = DateMatiere;
                matiere.DureeH = DureeH;
                matiere.DureeM = DureeM;
                matiere.HeureDebutH = HeureDebutH;
                matiere.HeureDebutM = HeureDebutM;
                this.matiereExamenService.Update(matiere);
                msg = "Enregistrement effectué avec succès!";
            }
            catch (Exception exp)
            {
                msg = "Echec de l'enregistrement!";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValiderExamen(string IdExam, string DateExamen, string nbrTotal, string nbrEligibleDateExam)
        {
            string msg = "";
            try
            {
                Examen exam= examenService.GetById(Convert.ToInt32(IdExam));
                exam.DateExamen = Convert.ToDateTime(DateExamen);
                exam.nbrCandidatEligibleAnnee = Convert.ToInt32(nbrTotal);
                exam.nbrCandidatEligibleDateExam = Convert.ToInt32(nbrEligibleDateExam);
                examenService.Update(exam);
                msg = "Validation effectuée avec succès!";
            }
            catch (Exception exp)
            {
                msg = "Echec de l'enregistrement!";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //Générer lz rapport liste 

        public FileStreamResult GenerateAndDisplayReport()
        {
            //int idCom = Convert.ToInt32(Session["commissionId"]);
            //var convocationUriString = Url.Action("Convocation", "GestionCommission", new { id = membreId, idReunion = reunionId, idCom = idCom }, protocol: Request.Url.Scheme);

            //var pdf = ConvertHtmlToPdf(convocationUriString);
            //// MemoryStream ms = new MemoryStream(pdf);

            //Response.Clear();
            //MemoryStream ms = new MemoryStream(pdf);
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=convocation.pdf");
            //Response.Buffer = true;
            //ms.WriteTo(Response.OutputStream);
            //Response.End();
            ////File f = new FileStreamResult(ms, "application/pdf");
            //Session["file"] = pdf;

            //return new FileStreamResult(ms, "application/pdf");


            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Rapport/Report_AGEn3.rdlc");
            

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet_AGEN3";

            List<ResultatRapportDto> examenfilterList = examenService.GetListExamen();
            foreach (ResultatRapportDto exam in examenfilterList)
            { 
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-MA");
            DateTime date = Convert.ToDateTime(exam.DateExamen);
            exam.DateExamenString = date.ToString("dd MMMM yyyy  ", CultureInfo.CreateSpecificCulture("ar-MA")).Replace(",", "");
            }
            reportDataSource.Value = examenfilterList;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>pdf</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);

            Response.Clear();
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=convocation.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            return new FileStreamResult(ms, "application/pdf");
            //return File(renderedBytes, "pdf");

        }

        public ActionResult EnvoyerEmail(Tuple<CritereRechercheDto, IEnumerable<CandidatResultatDto>, IEnumerable<MatiereDto>, Email> param)
        {
            var file = (byte[])Session["file"];
            if (file == null)
            {
                int idCom = Convert.ToInt32(Session["commissionId"]);
                var convocationUriString = Url.Action("GenerateAndDisplayReport", "AvancementExamen", null, protocol: Request.Url.Scheme);

                file = FileUtilsService.ConvertHtmlToPdf(convocationUriString);
            }

            try
            {
                Attachment att = new Attachment(new MemoryStream(file), "Convocation");
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);

                mail.From = new MailAddress(ConfigurationManager.AppSettings["SmtpAdress"]);
                mail.To.Add(param.Item4.Destinataire);
                mail.Subject = param.Item4.Objet;
                mail.Body = param.Item4.Message;
                mail.Attachments.Add(att);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpAdress"], ConfigurationManager.AppSettings["SmtpPassWord"]);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);


            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("ParametrageCap", new { onglet = "membre" });
        }

        public ActionResult AvExamenGestionQuota(Tuple<CandidatCritereRechDto, IEnumerable<QuotaDto>> dto)
        {
            //Alimentation des listes des années 
            List<String> listAnnee = new List<string>();
            List<QuotaDto> list = new List<QuotaDto>();
            ViewBag.affiche = false; 
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            List<ListEnum> listEnum = new List<ListEnum>();
            var valuesEnum = Enum.GetValues(typeof(EtatQuota)).Cast<EtatQuota>();
            foreach (var r in valuesEnum)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                ListEnum element = new ListEnum();
                element.Code = r.ToString();
                element.Description = attributes[0].Description.ToString();
                listEnum.Add(element);

            }


            ViewBag.Annee = new SelectList(listAnnee, dto.Item1.AnneeProm);
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description", dto.Item1.GradeId);
            //ViewBag.Statuts = new SelectList(listEnum, "Code", "Description");
            if (dto.Item1.AnneeProm != null && dto.Item1.GradeId != 0)
            {
                List<QuotaDto> tableauQuota = parametrageQuotaService.GetTableauQuotaAE(dto.Item1.AnneeProm, dto.Item1.GradeId);
                QuotaDto tableauQuotaAC = parametrageQuotaService.GetNbrPosteAC(dto.Item1.AnneeProm, dto.Item1.GradeId);
                if (tableauQuota.Count == 0)
                {
                    ModelState.AddModelError("msgError", "Veuillez paramétrer le quota avant de générer le tableau des quotas.");

                }else if(tableauQuotaAC == null)
                {
                    ModelState.AddModelError("msgError", "Veuillez générer le tableau des quotas au choix avant de générer le tableau des quotas sur examens.");
                }
                else
                {
                    int nbrPromouvable = detailAvancementService.GetNombreTotalPromouvableAE(dto.Item1.GradeId, dto.Item1.AnneeProm);
                    foreach (QuotaDto quota in tableauQuota)
                    {
                        quota.NbrPosteOuvrir = Math.Round((nbrPromouvable - tableauQuotaAC.NbrPosteOuvrir) * quota.Quota / 100);
                        ParametrageQuota param = parametrageQuotaService.GetById(Convert.ToInt32(tableauQuota.First().id));
                        param.NbrPoste = quota.NbrPosteOuvrir;
                        parametrageQuotaService.Update(param);
                        quota.Statut = new SelectList(listEnum, "Code", "Description", quota.StatutTQ);
                        list.Add(quota);
                    }
                    ViewBag.affiche = true;
                }
            }
            dto = new Tuple<CandidatCritereRechDto, IEnumerable<QuotaDto>>(dto.Item1, list);
            return View(dto);
        }

        //Retourne la liste des candidats pour avancement au choix
        public ActionResult AvExamenGestionCandidatures(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> dto, string candidatId)
        {

            //Syntese cnadidat
            ViewBag.rendredSyntese = false;
            CandidatResultatDto candidatSynthese = new CandidatResultatDto();
          
            if (!String.IsNullOrEmpty(candidatId))
            {
                int id = Int32.Parse(candidatId);             
                candidatSynthese = candidatService.GetSyntheseCandidat(id);
                ViewBag.rendredSyntese = true;
            }
            
            //récupération de la liste des candidats par critères de recherche
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();

            if (dto.Item2.GradeId == 0 && dto.Item2.AncienneteGrade == 0 && dto.Item2.SpecialiteId == 0 &&
                dto.Item2.DateRecrutement == null && dto.Item2.AnneeProm == null && ("Selectionnez").Equals(dto.Item2.EtatCandidature.ToString()))
            {
                candidats = new List<CandidatResultatDto>();
               
            }
            else
            {
                candidats = candidatService.GetAllCandidatures(dto.Item2);
            }

            //Alimentation des listes de choix 
            List<String> listAnnee = new List<string>();
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }

            //Chargement des listes de choix
            ViewBag.Specialites = new SelectList(specialiteService.GetAll(), "Id", "Intitule", dto.Item2.SpecialiteId);
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description", dto.Item2.GradeId);
            ViewBag.Localites = new SelectList(localiteService.GetAll(), "Id", "Intitule", dto.Item2.GradeId);
            ViewBag.Examens = new SelectList(examenService.GetAll(), "Id", "Intitule", dto.Item2.GradeId);
            ViewBag.Annee = new SelectList(listAnnee, dto.Item2.AnneeProm);

            //Return value
            Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> tuple = new Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto>(candidats, new CandidatCritereRechDto(), candidatSynthese, new CandidatDto());
            return View(tuple);
        }


        //Suppression candidat
        public ActionResult SupprimerCandidature(string Id)
        {
            Candidature candidature = candidatureService.GetById(Convert.ToInt32(Id));
            candidatureService.Delete(candidature);
            return RedirectToAction("AvExamenGestionCandidatures");
        }

        //Modifcation note
        public ActionResult editerNote(string detailAvancementId)
        {
            ViewBag.detailAvancementId = detailAvancementId;
            DetailAvancement dto = new DetailAvancement();
            dto = detailAvancementService.GetById(int.Parse(detailAvancementId));
            var note = dto.Note;
            var result = new { data = note };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult modifierNote(string detailAvancementId)
        {
            return RedirectToAction("AvExamenGestionCandidatures");
        }

        //Modifier la moyenne d'un candidat
        public ActionResult UpdateMoyenneCandidat(string moyenneValue, string avancementDetailId)
        {
            string msgInfo = "";
            string msgError = "";
            if (moyenneValue.All(char.IsDigit))
            {
                decimal note = decimal.Parse(moyenneValue.Replace(".", ","));
                if (note <= 20)
                {
                    detailAvancementService.modifyNoteMoyenne(moyenneValue, avancementDetailId);
                    msgInfo = "Modification effectué avec succès";
                    //Retern value
                    return Json(new { data = moyenneValue, messageInfo = msgInfo, messageError = msgError }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    msgError = "La note doit être comprise entre 0 et 20 !!";
                    return Json(new { data = "0", messageInfo = msgInfo, messageError = msgError }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                msgError = "Veuillez entrer une note valide !!";
                return Json(new { data = "0", messageInfo = msgInfo, messageError = msgError }, JsonRequestBehavior.AllowGet);
            }
        }

      

        [HttpPost]
        public ActionResult creerCandidature(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> dto)
        {
            CandidatDto candidat = dto.Item4;
            candidatureService.creerCandidature(candidat);
            return RedirectToAction("AvExamenGestionCandidatures");
        }

        public JsonResult ValiderNotation(string IdDetail, string noteEcrite, string notePresConnaissanceG, string noteConnaissMin, string noteConnaissSp, string noteOrale, string noteGlobale)
        {
            string msg = "";
            try { 
                DetailAvancement detail = detailAvancementService.GetById(Convert.ToInt32(IdDetail));
                Notation notation = new Notation();
                notation.NoteEcrite = Convert.ToInt32(noteEcrite);
                notation.NotePresConnaissanceG = Convert.ToInt32(notePresConnaissanceG);
                notation.NoteConnaissMin = Convert.ToInt32(noteConnaissMin);
                notation.NoteConnaissSp = Convert.ToInt32(noteConnaissSp);
                notation.NoteOrale = Convert.ToInt32(noteOrale);
                notation.NoteGlobale = Convert.ToInt32(noteGlobale);
                notation.DetailAvancement = detail;
                notationService.Create(notation);
                msg = "Validation effectuée avec succès!";
                return Json(msg, JsonRequestBehavior.AllowGet);
                }
            catch(Exception exp)
            {
                msg="Echec de validation! Veuillez contacter votre administrateur!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }


        }

        //Suppression candidat
        public ActionResult SupprimerCandidat(string id)
        {
            DetailAvancement detail = detailAvancementService.GetById(Convert.ToInt32(id));
            detailAvancementService.Delete(detail);
            Session["source"] = "delete";
            return RedirectToAction("AvExamenGestionCandidats");
        }

        public JsonResult setStatutTableauQuota(int id, string statut)
        {
            string msg = "";
            try
            {
                ParametrageQuota param = parametrageQuotaService.GetById(id);
                param.Statut = statut;
                parametrageQuotaService.Update(param);
                msg = "Validaion effectuée avec succées!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exp)
            {
                msg = "Echecs de la validation! Veuillez contacter votre administrateur!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        //Générer lz rapport liste 

        public ActionResult ArreteOuverture(string IdExam)
        {
            Examen examen = examenService.GetById(Convert.ToInt32(IdExam));
            DateTime dateLimite = (DateTime)examen.Datelimite;
            ViewBag.date = dateLimite.ToString("D", CultureInfo.CreateSpecificCulture("ar-MA")).Replace(",", "");
            QuotaDto param = parametrageQuotaService.GetNbrPosteAE(examen.Annee, Convert.ToInt32(examen.Grade.Id));
            ViewBag.nbrPoste = param.NbrPosteOuvrir;
            ViewBag.nbrPosteL = InternationalNumericFormatter.FormatWithCulture("L", param.NbrPosteOuvrir, null, new CultureInfo("ar-AE"));

            DateTime dateMatiereRef= new DateTime();
            int nbrJour = 0;
            string DateMatiereString = "";
            string DateMatiereMonthString = "";
            string DateMatiereYearString = "";
            string NbrJourString = "";
            
            
            List<DateTime> listSameYearDiffMonth = new List<DateTime>();
            List<DateTime> listSameYearSameMonth = new List<DateTime>();
            List<DateTime> listDiffYear = new List<DateTime>();
            List<MatiereExamen> list = examen.ListMatiereExamen;
            if(list != null)
            {
                list = list.OrderBy(x => x.DateMatiere).ToList();
                dateMatiereRef = Convert.ToDateTime(list.First().DateMatiere);
                nbrJour++; 
            }
            
            
            foreach (MatiereExamen matiere in list)
            {

               if(dateMatiereRef.Year != Convert.ToDateTime(matiere.DateMatiere).Year)
                {
                  listDiffYear.Add(Convert.ToDateTime(matiere.DateMatiere));
                  nbrJour++;
                }
               else
                {
                    if (dateMatiereRef.Year == Convert.ToDateTime(matiere.DateMatiere).Year && dateMatiereRef.Month != Convert.ToDateTime(matiere.DateMatiere).Month)
                    {
                        listSameYearDiffMonth.Add(Convert.ToDateTime(matiere.DateMatiere));
                        nbrJour++;
                    }
                    if (dateMatiereRef.Year == Convert.ToDateTime(matiere.DateMatiere).Year && dateMatiereRef.Month == Convert.ToDateTime(matiere.DateMatiere).Month && dateMatiereRef.Day != Convert.ToDateTime(matiere.DateMatiere).Day)
                    {
                        listSameYearSameMonth.Add(Convert.ToDateTime(matiere.DateMatiere));
                        nbrJour++;
                    }
                }

            }

            DateMatiereString = dateMatiereRef.ToString("dddd dd", CultureInfo.CreateSpecificCulture("ar-MA")).Replace(",", "");
            DateMatiereMonthString = dateMatiereRef.ToString("MMMM", CultureInfo.CreateSpecificCulture("ar-MA")).Replace(",", "");
            DateMatiereYearString = dateMatiereRef.ToString("yyyy", CultureInfo.CreateSpecificCulture("ar-MA")).Replace(",", "");


            foreach (DateTime date in listSameYearSameMonth)
            {
                DateMatiereString = DateMatiereString + " و  " + date.ToString("dddd dd", CultureInfo.CreateSpecificCulture("ar-MA")).Replace(",", "");
            }
            DateMatiereString = DateMatiereString + " " + DateMatiereMonthString;

            foreach (DateTime date in listSameYearDiffMonth)
            {
                DateMatiereString = DateMatiereString + " و  " + date.ToString("dddd dd MMMM", CultureInfo.CreateSpecificCulture("ar-MA")).Replace(",", "");
            }

            DateMatiereString = DateMatiereString + " " + DateMatiereYearString;

            foreach (DateTime date in listDiffYear)
            {
                DateMatiereString = DateMatiereString + " و  " + date.ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("ar-MA")).Replace(",", "");
            }


           
            if(nbrJour == 1)
            {

                NbrJourString = "يوم" + DateMatiereString;
            }
            else if(nbrJour == 2)
            {
                NbrJourString = "يومي" + DateMatiereString;
            }
            else
            {
                NbrJourString = " أيام " + DateMatiereString;
            }
            ViewBag.DateExamenString = NbrJourString;     
            return View(examen); 

        }

        public ActionResult GenererArreteOuverture(string IdExam)
        {
            int idCom = Convert.ToInt32(Session["commissionId"]);
            var convocationUriString = Url.Action("ArreteOuverture", "AvancementExamen",new {IdExam = IdExam}, protocol: Request.Url.Scheme);

            var pdf = FileUtilsService.ConvertHtmlToPdf(convocationUriString);
            // MemoryStream ms = new MemoryStream(pdf);

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdf);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=convocation.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
            //File f = new FileStreamResult(ms, "application/pdf");
            Session["file"] = pdf;

            return new FileStreamResult(ms, "application/pdf");

        }


        public FileStreamResult GenerateAndDisplayReportAGEN2(string quotaId)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Rapport/Report_AGEn2.rdlc");


            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet_AGEN2";

            ParametrageQuota param = parametrageQuotaService.GetById(Convert.ToInt32(quotaId)); 
            Examen exam = examenService.GetByAnneeAndIdGrade(param.Annee, Convert.ToInt32(param.GradeAcces.Id));
            QuotaDto quotaDto = parametrageQuotaService.GetNbrPosteAC(param.Annee, Convert.ToInt32(param.GradeAcces.Id));
            List<ResultatRapportDto> paramList = new List<ResultatRapportDto>();
            ResultatRapportDto parametre = new ResultatRapportDto(); 
                parametre.IntituleGrade = param.GradeAcces.DescriptionAM;
                parametre.nbrTotalannee = exam.nbrCandidatEligibleAnnee;
                parametre.AnneeExam = exam.Annee;
                //"إمكانية" + Math.Round(param.NbrPoste) + "تحتسب" 
                parametre.Formule = param.NbrPoste + "=%" + param.Quota + "*(" + Math.Round(quotaDto.NbrPosteOuvrir) + "-" + exam.nbrCandidatEligibleAnnee + ")" + "تحتسب" + Math.Round(param.NbrPoste) + "إمكانية";
                parametre.NbrPosteOuvrir = Convert.ToInt32(quotaDto.NbrPosteOuvrir);
            paramList.Add(parametre);
            reportDataSource.Value = paramList;

            localReport.DataSources.Add(reportDataSource);
            string reportType = "pdf";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>pdf</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.Clear();
            MemoryStream ms = new MemoryStream(renderedBytes);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=AGEn2.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            return new FileStreamResult(ms, "application/pdf");

        }

        [HttpPost]
        [LogTraceAttribute(Action = "Création manuelle")]
        public ActionResult creerCandidat(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> dto)
        {
            CandidatDto candidat = dto.Item4;
            //OdbcConnection connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");
            OdbcConnection connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
            DetailAvancement detail = candidatService.creerCandidatExamen(connection, candidat);
            connection.Dispose();
            Session["source"] = "creation";
            // list des id pour historisation 
            List<long> listId = new List<long>();
            listId.Add(detail.Id);
            ViewBag.listId = listId;
            return RedirectToAction("AvExamenGestionCandidats");
        }

        [HttpPost]
        [LogTraceAttribute(Action = "Changement statut")]
        public ActionResult changerStatut(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> model)
        {
            List<long> listId = new List<long>();
            List<CandidatResultatDto> candidats = model.Item1.Where(x => x.Selected).ToList();

            if (candidats.Any())
            {
                if (Request.Form["validationAction"] != null)
                {
                    candidats = candidats.Where(x => x.Statut == "Verifie").ToList();
                    if (candidats.Any())
                    {
                        foreach (CandidatResultatDto candidat in candidats)
                        {
                            detailAvancementService.modifyStatut(EtatDetailAVC.Valide.ToString(), candidat.detailAvancement);
                            TempData["successMessage"] = "la validation a été effectuée avec succès";
                            listId.Add(candidat.detailAvancement);
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Veuillez choisir un candidat ayant le statut vérifié!!";
                    }

                }

                else if (Request.Form["verificationAction"] != null)
                {
                    candidats = candidats.Where(x => x.Statut == "EnCours").ToList();
                    if (candidats.Any())
                    {
                        foreach (CandidatResultatDto candidat in candidats)
                        {
                            detailAvancementService.modifyStatut(EtatDetailAVC.Verifie.ToString(), candidat.detailAvancement);
                            TempData["successMessage"] = "la vérification a été effectuée avec succès";
                            listId.Add(candidat.detailAvancement);
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Veuillez choisir un candidat ayant le statut en cours!!";
                    }
                }

                else if (Request.Form["decisionAction"] != null)
                {
                    candidats = candidats.Where(x => x.Statut == "Valide").ToList();

                    if (candidats.Any())
                    {
                        foreach (CandidatResultatDto candidat in candidats)
                        {
                            CandidatDto dto = model.Item4;

                            string decision = "";
                            string motifDecision = "";

                            if ("F".Equals(dto.decision))
                            {
                                decision = EtatDetailAVC.Retenu.ToString();
                                motifDecision = dto.motifDecision;
                            }

                            if ("DF".Equals(dto.decision))
                            {
                                decision = EtatDetailAVC.NonRetenu.ToString();
                                motifDecision = dto.motifDecision;
                            }

                            detailAvancementService.modifierDecisionCap(decision, motifDecision, candidat.detailAvancement);
                            listId.Add(candidat.detailAvancement);
                            TempData["successMessage"] = "la modification a été effectuée avec succès";
                        }
                    }

                    else
                    {
                        TempData["ErrorMessage"] = "Veuillez choisir un candidat ayant le statut validé !!";
                    }
                }
            }
            else
            {

                TempData["ErrorMessage"] = "Veuillez choisir un candidat !!";
            }
            Session["source"] = "verifie";
            ViewBag.listId = listId;
            return RedirectToAction("AvExamenGestionCandidats");
        }

        //Chargement du candidat de GipeOrd
        public ActionResult GetCandidatGipeOrdByNumDoti(string numDoti)
        {
            OdbcConnection connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
            //OdbcConnection connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");
            //Initialisation des variables
            CandidatDto dto = new CandidatDto();
            string msgError = "";
            Boolean disabledAdd = true;

            //Si le N° Dotti est valide
            if (!String.IsNullOrEmpty(numDoti))
            {
                if (numDoti.All(char.IsDigit))
                {
                    //Chargement du candidat de GipeOrd
                    dto = canidatGOService.GetCandidatByNumDoti(connection, numDoti);

                    //récupération du grade concernés   
                    if (dto != null)
                    {
                        GradeDto grade = gradeService.GetGradeDtoByCode(dto.CodeCateg, dto.CodeCorps, dto.CodeCadre, dto.CodeGrade);
                        dto.AncienGrade = grade != null ? grade.Description : "";
                        disabledAdd = false;
                    }

                    //Si le N° Dotti n'existe pas 
                    if (dto == null)
                    {
                        dto = new CandidatDto();
                        msgError = "Candidat inexistant dans la base de données GipeOrd !!";
                        disabledAdd = true;
                    }

                    //Si le N° Dotti existe dans la base
                    else
                    {
                        //Validation de l'unicité du candidat dans la base SIRH
                        bool isExistingCandidat = candidatService.isExistingCandidat(numDoti);

                        //Candidat existant ==> erreur d'unicité
                        if (isExistingCandidat)
                        {
                            dto = new CandidatDto();
                            msgError = "Candidat en question dispose d'un avancement en cours !!";
                            disabledAdd = true;
                        }
                    }
                }

                //N° Dotti invalide 
                else
                {
                    msgError = "N° Dotti invalide !!";
                    disabledAdd = true;
                }
            }
            connection.Dispose();
            //Retern value
            var result = new { data = dto, message = msgError, flag = disabledAdd };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       

        //Charegement du candidat à modifier
        [LogTraceAttribute(Action = "Candidat mis à jour")]
        public ActionResult editerCandidat(string candidatId, string detailAvancementId)
        {
            CandidatDto dto = new CandidatDto();
            // Specify Data Source Name DSN along with user and password
            //OdbcConnection connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");
            OdbcConnection connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
            dto = candidatService.GetCandidatByNumDotti(connection, candidatId, int.Parse(detailAvancementId));
            connection.Dispose();
            List<long> listId = new List<long>();
            listId.Add(int.Parse(detailAvancementId));
            ViewBag.listId = listId;
            var result = new { data = dto };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Modification du candidat
        [HttpPost]
        [LogTraceAttribute(Action = "Candidat mis à jour")]
        public ActionResult modifierCandidat(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> dto)
        {
            CandidatDto candidat = dto.Item4;
            OdbcConnection connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
            //OdbcConnection connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");
            candidatService.modifierCandidat(connection, candidat);
            connection.Dispose();
            List<long> listId = new List<long>();
            listId.Add(candidat.detailAvancementId);
            ViewBag.listId = listId;
            Session["source"] = "modification";
            return RedirectToAction("AvExamenGestionCandidats");
        }




        // Fonction permettant de générer une convocation 

        public ActionResult GenererConvocation(string candidatId, string idDetail)
        {
            var convocationUriString = Url.Action("Convocation", "AvancementExamen", new { id = candidatId, detailId = idDetail }, protocol: Request.Url.Scheme);

            var pdf = FileUtilsService.ConvertHtmlToPdf(convocationUriString);
            // MemoryStream ms = new MemoryStream(pdf);

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdf);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=convocation.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
            //File f = new FileStreamResult(ms, "application/pdf");
            Session["file"] = pdf;

            return new FileStreamResult(ms, "application/pdf");

        }

        public ActionResult Convocation(string id, string detailId)
        {
            Candidat m = candidatService.GetById(Convert.ToInt32(id));
            DetailAvancement c = detailAvancementService.GetById(Convert.ToInt32(detailId));
            Examen exam = examenService.GetByAnneeAndIdGrade(c.Annee, Convert.ToInt32(c.GradeIdNouveau));
            List<MatiereDto> listMatiere = new List<MatiereDto>();
            if (exam != null)
            {
                foreach (MatiereExamen matiere in exam.ListMatiereExamen)
                {
                    MatiereDto matiereDto = new MatiereDto();
                    matiereDto.Intitule = matiere.Matiere.Intitule;
                    matiereDto.JourDateMatiere = Convert.ToDateTime(matiere.DateMatiere).ToString("dddd", CultureInfo.CreateSpecificCulture("fr-FR")).Replace(",", "");
                    matiereDto.Coefficient = matiere.Coefficient;
                    System.TimeSpan duration = new System.TimeSpan(Convert.ToInt32(matiere.HeureDebutH), Convert.ToInt32(matiere.HeureDebutM), 0);
                    System.TimeSpan durationAdd = new System.TimeSpan(Convert.ToInt32(matiere.DureeH), Convert.ToInt32(matiere.DureeM), 0);
                    duration = duration.Add(durationAdd);
                    matiereDto.HeureDebutH = "de " + matiere.HeureDebutH + "h " + matiere.HeureDebutM + " à " + duration.ToString(@"hh") + " h " + duration.ToString(@"mm");
                    listMatiere.Add(matiereDto);

                }
            }
            return View(new Tuple<Candidat, DetailAvancement, Examen, IEnumerable<MatiereDto>>(m, c, exam, listMatiere));

        }

        /*
      * Connects to the specified ODBC source and returns the connection reference
      */
        protected OdbcConnection GetConnection(string connectString)
        {
            OdbcConnection connection = null;

            // Open a connection
            try
            {
                connection = new OdbcConnection(connectString);
                connection.Open();
            }
            catch (OdbcException ex)
            {
                var exp = ex.Message;
                return null;
            }
            return connection;
        }

        // Fonction permettant de générer une convocation 

        public ActionResult GenerateConvocation(string candidatId, string detailId)
        {
            var convocationUriString = Url.Action("Convocation", "AvancementExamen", new { id = candidatId, detailId = detailId }, protocol: Request.Url.Scheme);

            var pdf = ConvertHtmlToPdf(convocationUriString);
            // MemoryStream ms = new MemoryStream(pdf);

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdf);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=convocation.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
            //File f = new FileStreamResult(ms, "application/pdf");
            Session["file"] = pdf;

            return new FileStreamResult(ms, "application/pdf");

        }



        public static byte[] ConvertHtmlToPdf(string url)
        {

            const string fileName = " - ";
            const string wkhtmlDir = @"C:\Program Files\wkhtmltopdf\";
            const string wkhtml = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe";
            var p = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    FileName = wkhtml,
                    WorkingDirectory = wkhtmlDir
                }
            };

            var switches = "";
            switches += "--print-media-type ";
            switches += "--margin-top 10mm --margin-bottom 10mm --margin-right 10mm --margin-left 10mm ";
            switches += "--page-size Letter ";
            p.StartInfo.Arguments = switches + " " + url + " " + fileName;
            p.Start();

            //read output
            var buffer = new byte[32768];
            byte[] file;
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    var read = p.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length);

                    if (read <= 0)
                    {
                        break;
                    }
                    ms.Write(buffer, 0, read);
                }
                file = ms.ToArray();
            }
            // wait or exit
            p.WaitForExit(60000);
            p.Close();
            return file;
        }

        [LogTraceAttribute(Action = "Convocation envoyée")]
        public ActionResult EnvoyerEmail(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> dto,int detailId, string candidatId)
        {
            var file = (byte[])Session["file"];
            if (file == null)
            {
                var convocationUriString = Url.Action("Convocation", "AvancementExamen", new { id = candidatId, detailId = detailId }, protocol: Request.Url.Scheme);

                file = ConvertHtmlToPdf(convocationUriString);
            }

            try
            {
                Attachment att = new Attachment(new MemoryStream(file), "Convocation");
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);

                mail.From = new MailAddress(ConfigurationManager.AppSettings["SmtpAdress"]);
                mail.To.Add(dto.Item4.Email);
                mail.Subject = dto.Item4.Convocation.Objet;
                mail.Body = dto.Item4.Convocation.Message;
                mail.Attachments.Add(att);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpAdress"], ConfigurationManager.AppSettings["SmtpPassWord"]);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                List<long> listId = new List<long>();
                listId.Add(detailId);
                ViewBag.listId = listId;


            }
            catch (Exception ex)
            {

            }

            Session["source"] = "envoieMail";
            return RedirectToAction("AvExamenGestionCandidats");
        }
        
      
   	}
}
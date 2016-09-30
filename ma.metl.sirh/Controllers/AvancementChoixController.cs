using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Data.Odbc;
using ma.metl.sirh.Common.TraceHistory;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Diagnostics;

namespace ma.metl.sirh.Controllers
{
    public class AvancementChoixController : BaseController
    {

        IGradeService gradeService;
        ILocaliteService localiteService;
        ICandidatService candidatService;
        ICandidatGOService canidatGOService;
        IDetailAvancementService detailAvancementService;
        IParametrageQuotaService parametrageQuotaService;
        IHistoriqueService historiqueService;

        public AvancementChoixController(IGradeService gradeService, ILocaliteService localiteService, ICandidatService candidatService, IDetailAvancementService detailAvancementService, ICandidatGOService canidatGOService, IParametrageQuotaService parametrageQuotaService, IHistoriqueService historiqueService)
            : base(historiqueService, detailAvancementService)
        {
            this.gradeService = gradeService;
            this.localiteService = localiteService;
            this.candidatService = candidatService;
            this.detailAvancementService = detailAvancementService;
            this.canidatGOService = canidatGOService;
            this.parametrageQuotaService = parametrageQuotaService;
            this.historiqueService = historiqueService;
        }

        public ActionResult Index()
        {
            return View();
        }

        //Retourne la liste des candidats pour avancement au choix
        public ActionResult AvChoixGestionCandidats(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> dto, string candidatId, string moyenne, string detail, string statut, string name)
        {
           
            //récupération de la liste des candidats par critères de recherche
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();
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

            if (dto.Item2.GradeId == 0 && dto.Item2.RegionId == 0 && dto.Item2.AncienneteEchelon == 0
                    && dto.Item2.AncienneteGrade == 0
                    && dto.Item2.NumDoti == null && dto.Item2.DateNaissance == null
                    && dto.Item2.DateRecrutement == null && dto.Item2.AnneeProm == null)
            {
                candidats = new List<CandidatResultatDto>();
                candidatId = null;
            }

            else
            {
                candidats = candidatService.GetAllCandidatAuChoix(dto.Item2);
            }

            //Syntese cnadidat
            ViewBag.rendredSyntese = false;

            CandidatResultatDto candidatSynthese = new CandidatResultatDto();
            if (candidatId != null && candidatId != "" && name.Equals("syntese"))
            {
                int id = Int32.Parse(candidatId);
                string detailAvancement = detail;
                decimal note = decimal.Parse(moyenne.Replace(".", ","));
                candidatSynthese = candidatService.GetSyntheseCandidat(id);
                ViewBag.rendredSyntese = true;
                ViewBag.noteMoyenne = note;
                ViewBag.detailAvancementId = detailAvancement;
                candidats = candidatService.GetCandidatAuChoixById(id);           
                candidatSynthese.Historiques = historiqueService.GetByIdDetailAvancement(Convert.ToInt32(detailAvancement));
            }
            
            string errorMessage = TempData["ErrorMessage"] as string;
            string successMessage = TempData["successMessage"] as string;               

            if (!String.IsNullOrEmpty(errorMessage) || !String.IsNullOrEmpty(successMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
                ViewBag.successMessage = successMessage;
                candidats = candidatService.GetAllCandidatAuChoix();
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
           
            //Return value
            Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> tuple = new Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto>(candidats, new CandidatCritereRechDto(), candidatSynthese, new CandidatDto());
            return View(tuple);  
        }

        public ActionResult AvChoixGestionQuota(Tuple<CandidatCritereRechDto,IEnumerable<QuotaDto>> dto)
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
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x=>x.Description), "Id", "Description", dto.Item1.GradeId);
            //ViewBag.Statuts = new SelectList(listEnum, "Code", "Description");
            if(dto.Item1.AnneeProm != null && dto.Item1.GradeId != 0)
            {
                List<QuotaDto> tableauQuota = parametrageQuotaService.GetTableauQuotaAC(dto.Item1.AnneeProm, dto.Item1.GradeId);
                if (tableauQuota.Count == 0 )
                {
                    ModelState.AddModelError("msgError", "Veuillez paramétrer le quota avant de générer le tableau des quotas.");

                }
                else
                {
                    int nbrPromouvable = detailAvancementService.GetNombreTotalPromouvableAC(dto.Item1.GradeId, dto.Item1.AnneeProm);
                    foreach (QuotaDto quota in tableauQuota)
                    {
                        quota.NbrPosteOuvrir = Math.Round(nbrPromouvable * quota.Quota / 100);
                        quota.Statut = new SelectList(listEnum, "Code", "Description", quota.StatutTQ);
                        ParametrageQuota param = parametrageQuotaService.GetById(Convert.ToInt32(quota.id));
                        param.NbrPoste = quota.NbrPosteOuvrir;
                        parametrageQuotaService.Update(param);
                        list.Add(quota);
                    }
                    ViewBag.affiche = true;
                }
            }

            dto = new Tuple<CandidatCritereRechDto, IEnumerable<QuotaDto>>(dto.Item1, list);
            return View(dto);
        }
     
        //Suppression candidat
        public JsonResult SupprimerCandidat(string id)
        {
            Session["source"] = "delete";
            detailAvancementService.deleteAvancement(Convert.ToInt32(id));
            bool isDeleted = true;
            string msgDeleteInfoId = "Candidat supprimer avec succes";
            return Json(new { IsDeleted = isDeleted, msg = msgDeleteInfoId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [LogTraceAttribute(Action = "Création nouveau candidat")]
        public ActionResult creerCandidat(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> dto)
        {
            CandidatDto candidat = dto.Item4;
            //OdbcConnection connection = GetConnection("DSN=Connexion11g;UID=GIPEORD;PWD=GipeOrd");          
            OdbcConnection connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
            DetailAvancement detail = candidatService.creerCandidat(connection, candidat);
            connection.Dispose();
            Session["source"] = "creation";
            // list des id pour historisation 
            List<long> listId = new List<long>();
            listId.Add(detail.Id);
            ViewBag.listId = listId;
            return RedirectToAction("AvChoixGestionCandidats");
        }

        [HttpPost]
        [LogTraceAttribute(Action = "Changement de statut")]
        public ActionResult changerStatut(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> model)
        {
            List<long> listId = new List<long>();
            List<CandidatResultatDto> candidats = model.Item1.Where(x => x.Selected).ToList();

            if (candidats.Any()) {
                if (Request.Form["validationAction"] != null)
                {
                    candidats = candidats.Where(x => x.Statut == "Verifie").ToList();
                    if(candidats.Any()){
                        foreach (CandidatResultatDto candidat in candidats)
                            {
                                detailAvancementService.modifyStatut(EtatDetailAVC.Valide.ToString(), candidat.detailAvancement);
                                TempData["successMessage"] = "la validation a été effectuée avec succès";
                                listId.Add(candidat.detailAvancement);
                            }
                    }
                    else{
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
                            TempData["successMessage"] = "la modification a été effectuée avec succès";
                            listId.Add(candidat.detailAvancement);
                        }
                    }
                    
                    else
                    {
                        TempData["ErrorMessage"] = "Veuillez choisir un candidat ayant le statut validé !!";
                    }
                }
            }
            else {

                TempData["ErrorMessage"] = "Veuillez choisir un candidat !!";
            }
            Session["source"] = "verifie";
            ViewBag.listId = listId;
            return RedirectToAction("AvChoixGestionCandidats");
        }

        //Chargement du candidat de GipeOrd
        public ActionResult GetCandidatGipeOrdByNumDoti(string numDoti)
        {
            //OdbcConnection connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");
            OdbcConnection connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
           
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

        [HttpPost]
        [LogTraceAttribute(Action = "Modification de la note moyenne")]
        //Modifier la moyenne d'un candidat
        public ActionResult UpdateMoyenneCandidat(string moyenneValue, string avancementDetailId)
        {
            List<long> listId = new List<long>();
            listId.Add(int.Parse(avancementDetailId));
            ViewBag.listId = listId;
            Session["source"] = "modifierNote";
            string msgInfo = "";
            string msgError = "";

            try { 
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
            }catch(FormatException e){
                msgError = "Veuillez entrer une note valide !!";
                return Json(new { data = "0", messageInfo = msgInfo, messageError = msgError }, JsonRequestBehavior.AllowGet);
            }
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

        //Charegement du candidat à modifier
        [HttpPost]
        [LogTraceAttribute(Action = "Edition candidat")]
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
            var result = new { data = dto};
            return Json(result, JsonRequestBehavior.AllowGet);
        }   

        //Modification du candidat
        [HttpPost]
        [LogTraceAttribute(Action = "Modification candidat")]
        public ActionResult modifierCandidat(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto, CandidatResultatDto, CandidatDto> dto)
        {
            CandidatDto candidat = dto.Item4;
            //OdbcConnection connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");
            OdbcConnection connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");
            candidatService.modifierCandidat(connection, candidat);
            connection.Dispose();
            List<long> listId = new List<long>();
            listId.Add(candidat.detailAvancementId);
            ViewBag.listId = listId;
            Session["source"] = "modification";
            return RedirectToAction("AvChoixGestionCandidats");
        }
            // mettre à jour le statut des tableaux de quota 
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

        public FileContentResult GenerateAndDisplayReport(string annee, int gradeId, string format)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Rapport/Report_AGC2.rdlc");

            List<QuotaDto> list = new List<QuotaDto>();

            if (annee != null && gradeId != 0)
            {
                List<QuotaDto> tableauQuota = parametrageQuotaService.GetTableauQuotaAC(annee, gradeId);
                if (tableauQuota.Count != 0)
                {
                    int nbrPromouvable = detailAvancementService.GetNombreTotalPromouvableAC(Int32.Parse(annee), gradeId.ToString());
                    foreach (QuotaDto quota in tableauQuota)
                    {
                        quota.NbrPosteOuvrir = Math.Round(nbrPromouvable * quota.Quota / 100);
                        list.Add(quota);
                    }
                }
            }

            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", list);
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
            if (format == null)
            {
                return File(renderedBytes, "application/pdf");
            }
            else if (format == "PDF")
            {
                return File(renderedBytes, "pdf");
            }
            else
            {
                return File(renderedBytes, "image/jpeg");
            }
        }

        public ActionResult GenererCandidatDetails(string candidatId, string detailsId)
        {
            var candidatDetailsUriString = Url.Action("CandidatDetails", "AvancementChoix", new { id = candidatId, detailsId = detailsId }, protocol: Request.Url.Scheme);

            var pdf = FileUtilsService.ConvertHtmlToPdf(candidatDetailsUriString);
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

        public ActionResult CandidatDetails(string id, string detailsId)
        {
            List<CandidatResultatDto> candidatResultDtos = candidatService.GetCandidatAuChoixById(Convert.ToInt32(id));
            //foreach (var candidatResultDto in candidatResultDtos)
            //{
            //    candidatResultatDto.Intitule = matiere.Matiere.Intitule;
            //    matiereDto.JourDateMatiere = Convert.ToDateTime(matiere.DateMatiere).ToString("dddd", CultureInfo.CreateSpecificCulture("fr-FR")).Replace(",", "");
            //    matiereDto.Coefficient = matiere.Coefficient;
            //    System.TimeSpan duration = new System.TimeSpan(Convert.ToInt32(matiere.HeureDebutH), Convert.ToInt32(matiere.HeureDebutM), 0);
            //    System.TimeSpan durationAdd = new System.TimeSpan(Convert.ToInt32(matiere.DureeH), Convert.ToInt32(matiere.DureeM), 0);
            //    duration = duration.Add(durationAdd);
            //    matiereDto.HeureDebutH = "de " + matiere.HeureDebutH + "h " + matiere.HeureDebutM + " à " + duration.ToString(@"hh") + " h " + duration.ToString(@"mm");
            //}
            //listMatiere.Add(matiereDto);

            //    }
            //}
            int oldGradeId = candidatResultDtos.FirstOrDefault().GradeId;
            var oldGrade = gradeService.GetById(oldGradeId);
            if (oldGrade != null)
            {
                ViewBag.ArabicOldGradeName = oldGrade.DescriptionAM;
            }
            else
            {
                ViewBag.ArabicOldGradeName = "غير محدد";
            }

            int newGradeId = candidatResultDtos.FirstOrDefault().GradeId;
            var newGrade = gradeService.GetById(newGradeId);
            if (newGrade != null)
            {
                ViewBag.ArabicNewGradeName = newGrade.DescriptionAM;
            }
            else
            {
                ViewBag.ArabicNewGradeName = "غير محدد";
            }

            

            return View(candidatResultDtos);

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
	}
}
using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Service;
using Microsoft.International.Formatters;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ma.metl.sirh.Controllers
{
    public class GestionCommissionController : Controller
    {

        ICommissionService commissionService;
        IGradeService gradeService;
        IReunionService reunionService;
        IDirectionService directionService;
        IServiceService serviceService;
        IMembreCommissionService membreCommissionService;
        ICommissionMembreService commissionMembreService;


        public GestionCommissionController(ICommissionService commissionService, IGradeService gradeService, IReunionService reunionService, IDirectionService directionService, IServiceService serviceService, IMembreCommissionService membreCommissionService, ICommissionMembreService commissionMembreService)
        {
            this.commissionService = commissionService;
            this.gradeService = gradeService;
            this.reunionService = reunionService;
            this.directionService = directionService; 
            this.serviceService = serviceService;
            this.membreCommissionService = membreCommissionService;
            this.commissionMembreService = commissionMembreService;
           
        }

        // Parametrage Commission 

        public ActionResult ParametrageCap(string commissionId, string onglet, string msgInfo, string msgErreur, string operation, string operationSup)
        {
            List<Reunion> listReunion = null;
            List<MembreCommission> listMembreBD = null;
            List<MembreCommission> listMembre = null;
            int idCom = 0;
            List<Commission> listCommission = commissionService.GetAll().ToList();
            if(commissionId != null)
            { 
            Session["commissionId"] = commissionId;
            idCom = Convert.ToInt32(commissionId);
            }else
            {
                idCom = Convert.ToInt32(Session["commissionId"]);
            }

            Commission commission = commissionService.GetById(idCom); 
            List<String> listAnnee = new List<string>();
            List<String> listTypeAvancement = new List<string>();
            // alimenter les listes 
            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            var valuesTypeAvancement = Enum.GetValues(typeof(TypeAvancement)).Cast<TypeAvancement>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            foreach (var v in valuesTypeAvancement)
            {
                var field = v.GetType().GetField(v.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listTypeAvancement.Add(attributes[0].Description.ToString());

            }
            ViewBag.Annee = new SelectList(listAnnee);
            ViewBag.TypeAvancement = new SelectList(listTypeAvancement);
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");
            ViewBag.AnneeM = new SelectList(listAnnee);
            ViewBag.TypeAvancementM = new SelectList(listTypeAvancement);
            ViewBag.GradesM = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");
            ViewBag.AnneeC = new SelectList(listAnnee);
            ViewBag.TypeAvancementC = new SelectList(listTypeAvancement);
            ViewBag.GradesC = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");
            ViewBag.Directions = new SelectList(directionService.GetAll(), "Id", "Description");
            ViewBag.Services = new SelectList(serviceService.GetAll(), "Id", "Description");
            ViewBag.Reunions = new SelectList(reunionService.GetReunionByDate(idCom), "Id", "DateReunion");

            if(commissionId != null && onglet == null)
            {
                ViewBag.showSynthese = "membre";
            }
            if (commissionId== null && onglet != null)
            {
                ViewBag.showSynthese = onglet;
            }
            if(onglet =="historique")
            {
                listReunion = reunionService.GetAll().Where(x => x.Commission.Id == idCom).ToList();
            }

            ViewModel viewModel = new ViewModel();
            viewModel.ListCommission = listCommission;
            viewModel.ListReunion = listReunion;

            //récupérer la liste des membres déjà enregistrée en base de données
            if (operationSup == "suppression")
            {
                listMembreBD = (List<MembreCommission>)Session["listMembreComplete"];
            }
            else
            {
                listMembreBD = membreCommissionService.GetMembresByCommissionId(idCom);
                listMembre = (List<MembreCommission>)Session["listMembre"];
                Session["listMembre"] = null;
            }
            
            if (listMembreBD.Count != 0 && listMembre != null)
            {
                foreach (MembreCommission m in listMembre)
                {
                    listMembreBD.Add(m);
                }
                viewModel.ListMembre = listMembreBD;
            }
            if (listMembre== null && listMembreBD.Count != 0)
            {
                viewModel.ListMembre = listMembreBD;
            }
            else if (listMembre != null && listMembreBD.Count == 0)
            {
                viewModel.ListMembre = listMembre;
            }
            if (listMembre == null && listMembreBD.Count == 0)
            {
                viewModel.ListMembre = new List<MembreCommission>();
            }
            Session["listMembreComplete"] = viewModel.ListMembre; 

            /*Message d'erreur*/
            if (msgInfo != null)
            {
                ModelState.AddModelError("msgInfo", msgInfo);
            }
            if(msgErreur != null)
            {
                ModelState.AddModelError("msgErreur", msgErreur);
            }

            return View(viewModel); 
        }
        /*Fonction permettant de créer une commission*/
        [HttpPost]
        public ActionResult CreerCommission(ViewModel param)
        {
                Commission commission = new Commission();
                Grade grade = gradeService.GetById(Convert.ToInt32(param.Commission.GradeId));
                commission.Titre = param.Commission.Titre;
                commission.EcritOrOral = param.Commission.EcritOrOral;
                commission.TypeAvancement = param.Commission.TypeAvancement;
                commission.Annee = param.Commission.Annee;
                commission.Grade = grade;
                commissionService.Create(commission);

                // récupérer l'id de la commission qu'on vient de créer
                long id = commission.Id;
                //inserer le lien entre commission et grade 
                //-------------------------------------------------------------
                /*
            
                for (int i = 0; i < Grades.Count(); i++)
                {
                    CommissionGrade commissionGrade = new CommissionGrade();
                    commissionGrade.Commission_Id = id;
                    commissionGrade.GradeId = Grades[i];
                    commissionGradeService.Create(commissionGrade); 
                }*/
                //-----------------------------------------------------
                return RedirectToAction("ParametrageCap");
        }

        /*Fonction permettant de modifier une commission*/
        [HttpPost]
        public ActionResult ModifierCommission(ViewModel param, string commissionIdM)
        {
            Commission commission = commissionService.GetById(Convert.ToInt32(commissionIdM));
            commission.Titre = param.Commission.Titre;
            commission.EcritOrOral = param.Commission.EcritOrOral;
            commission.Annee = param.Commission.Annee;
            commission.Grade = gradeService.GetById(Convert.ToInt32(param.Commission.GradeId));
            commission.TypeAvancement = param.Commission.TypeAvancement;
            commissionService.Update(commission);
            

            //----------------------------------------------------------
            /*

            List<CommissionGrade> list = commissionGradeService.GetByCommissionId(Convert.ToInt32(commissionIdM));

            foreach (CommissionGrade com in list)
            {
                commissionGradeService.Delete(com); 
            }

            for (int i = 0; i < GradesM.Count();i++ )
            {
                CommissionGrade commissionGrade = new CommissionGrade();
                commissionGrade.Commission_Id = Convert.ToInt32(commissionIdM);
                commissionGrade.GradeId = GradesM[i];
                commissionGradeService.Create(commissionGrade);
            }*/
            //------------------------------------------------------
                return RedirectToAction("ParametrageCap");
        }

        /*Fonction permettant de cloner une commission*/
        [HttpPost]
        public ActionResult ClonerCommission(ViewModel param)
        {
            Commission commission = new Commission();
            commission.Titre = param.Commission.Titre;
            commission.EcritOrOral = param.Commission.EcritOrOral;
            commission.Annee = param.Commission.Annee;
            commission.TypeAvancement = param.Commission.TypeAvancement;
            commission.Grade = gradeService.GetById(Convert.ToInt32(param.Commission.GradeId));
            commissionService.Create(commission);
            // récupérer l'id de la commission qu'on vient de créer
            long id = commission.Id; 
            //inserer le lien entre commission et grade 
            //-------------------------------------------
            /*
            for (int i = 0; i < GradesC.Count(); i++)
            {
                CommissionGrade commissionGrade = new CommissionGrade();
                commissionGrade.Commission_Id = id;
                commissionGrade.GradeId = GradesC[i];
                commissionGradeService.Create(commissionGrade);
            }*/
            //----------------------------------------------------
            return RedirectToAction("ParametrageCap");
        }

        public JsonResult getListeGrade(string comissionId)
        {
            try
            {

               /* List<CommissionGrade> listgrades = commissionGradeService.GetListGradeByCommissionId(Convert.ToInt32(comissionId));

                var grades = new List<object>();

                foreach (CommissionGrade c in listgrades)
                {
                    Grade grade = gradeService.GetById(Convert.ToInt32(c.GradeId));
                    grades.Add(new { Id = grade.Id, Name = grade.Description });
                }*/
                //return Json(grades, JsonRequestBehavior.AllowGet);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }


        public JsonResult getCommissionById(string comissionId)
        {
            try
            {
                Commission comm = commissionService.GetById(Convert.ToInt32(comissionId));

                /* List<CommissionGrade> listgrades = commissionGradeService.GetListGradeByCommissionId(Convert.ToInt32(comissionId));

                 var grades = new List<object>();

                 foreach (CommissionGrade c in listgrades)
                 {
                     Grade grade = gradeService.GetById(Convert.ToInt32(c.GradeId));
                     grades.Add(new { Id = grade.Id, Name = grade.Description });
                 }*/
                //return Json(grades, JsonRequestBehavior.AllowGet);
                return Json(comm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json("exception occured while processing your request");
            }
        }

        /*Fonction permettant de supprimer une commission*/
        
        public ActionResult SupprimerCommission(string commissionId)
        {
            Commission commission = commissionService.GetById(Convert.ToInt32(commissionId));
            commissionService.Delete(commission);
            return RedirectToAction("ParametrageCap"); 
        }

        /* Enregistrer commission */

        public ActionResult EnregistrerReunion(ViewModel param)
        {
            try
            {
                string msgInfo; 

                Reunion reunion = new Reunion();
                // récupérer le fichier à partir du view 
                HttpPostedFileBase file = Request.Files["flux"];
                string extension = Path.GetExtension(file.FileName);
                // récupérer le répertoire 
                string directoryInfo = ConfigurationManager.AppSettings["DirectoryInfo"];

                //renomer le fichier uploader 
                string destFilePath = @directoryInfo + "\\CompteRendu\\";
                string name = "CR_" + param.Reunion.DateReunion.Year + "" + param.Reunion.DateReunion.Month.ToString("00") + "" + param.Reunion.DateReunion.Day.ToString("00") + "" + extension;
                string cheminComplet = destFilePath + name;
                file.SaveAs(cheminComplet);

                //construire l'objet réunion

                reunion.DateReunion = param.Reunion.DateReunion;
                reunion.Decisions = param.Reunion.Decisions;
                reunion.Observation = param.Reunion.Observation;
                reunion.OrdreJour = param.Reunion.OrdreJour;
                reunion.Pv = cheminComplet;

                int idCommission = Convert.ToInt32(Session["commissionId"]);
                reunion.Commission = commissionService.GetById(idCommission);
                reunionService.Create(reunion);
                msgInfo = "L'opération est effectuée avec succès";
                return RedirectToAction("ParametrageCap", new { onglet = "observation", msgInfo }); 
            }catch(Exception e)
            {
                string msgErreur = "Erreur Technique! Merci de contacter votre administrateur";
                return RedirectToAction("ParametrageCap", new { onglet = "observation", msgErreur }); 
            }  
        }

        /*Fonction permettant de télécharger le pv de réunion*/

        public void TelechargerFichier(string FileId)
        {

            long id = Convert.ToInt32(FileId);
            Reunion reunion = reunionService.GetAll().Where(x => x.Id == id).First();
            string pathfile = reunion.Pv;

            FileInfo fileInfo = new FileInfo(pathfile);

            if (fileInfo.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + pathfile);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.ContentType = "application/msword";
                Response.Flush();
                Response.TransmitFile(pathfile);

            }

        }


        public JsonResult RechercherMembre(string direction, string service, string numDoti, string nom, string prenom)
        {
            List<MembreCommission> list = membreCommissionService.GetAll().ToList();
            List<MembreCommission> listExist =  (List<MembreCommission>)Session["listMembreComplete"];

            if(direction != "")
            {
                list = list.Where(x => x.Direction.Id == Convert.ToInt32(direction)).ToList(); 
            }
           /* if (service != "") à revoir
            {
                list = list.Where(x => x.Localite.Id == Convert.ToInt32(service)).ToList();
            }*/
            if (numDoti != "")
            {
                list = list.Where(x => x.NDoti == numDoti).ToList();
            }
            if (nom != "")
            {
                list = list.Where(x => x.nom == nom).ToList();
            }
            if (prenom != "")
            {
                list = list.Where(x => x.prenom == prenom).ToList();
            }

            foreach (MembreCommission m in listExist)
            {
                foreach (MembreCommission m1 in list)
                {
                    if (m.Id == m1.Id)
                    {
                        list.Remove(m1);
                        break;
                    }

                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AjouterMembresSelectionnes(string[] AliasChecked)
        {
            List<MembreCommission> list = new List<MembreCommission>(); 
            for (int i = 0; i < AliasChecked.Count(); i++)
            {
                MembreCommission membre = membreCommissionService.GetByNDoti(AliasChecked[i]);
                list.Add(membre); 
            }
            Session["listMembre"] = list;
            return RedirectToAction("ParametrageCap", new { onglet="membre", operation="ajout"}); 
        }

        public ActionResult Valider()
        {
            string msgErreur="";
            List<MembreCommission> listMembre = (List<MembreCommission>)Session["listMembreComplete"]; 
            if (listMembre != null)
            {
                int idCommission = Convert.ToInt32(Session["commissionId"]);

                List<CommissionMembre> list = commissionMembreService.GetByIdCommission(idCommission);
                foreach(CommissionMembre commission in list)
                {
                    commissionMembreService.Delete(commission);
                }

                for (int i = 0; i < listMembre.Count(); i++)
                {
                    CommissionMembre commissionMembre = new CommissionMembre();
                    commissionMembre.Commission_Id = idCommission;
                    commissionMembre.MembreCap_Id = Convert.ToInt32(listMembre[i].Id);
                    commissionMembreService.Create(commissionMembre);

                }
                Session["listMembre"] = null;
            }
            else
            {
                 msgErreur = "Veuillez sélectionner des membres de commissions avant de valider!";
            }
            return RedirectToAction("ParametrageCap", new { onglet = "membre", msgErreur }); 
        }

        public ActionResult SupprimerMembre(string numDoti)
        {

            List<MembreCommission> listMembre = (List<MembreCommission>)Session["listMembreComplete"];
            if (listMembre != null)
            {
                
                for (int i = 0; i < listMembre.Count(); i++)
                {
                    if (listMembre[i].NDoti == numDoti)
                        listMembre.Remove(listMembre[i]);

                }
                Session["listMembreComplete"] = listMembre;
                
            }
            
            return RedirectToAction("ParametrageCap", new { onglet = "membre", operationSup="suppression"});
        }
       
        public ActionResult CalendrierCap()
        {
            var tuple = new Tuple<ProgrammeModel, EventDto>(new ProgrammeModel(), new EventDto());
            
            // alimenter les listes 
            ViewBag.commission = new SelectList(commissionService.GetAll().ToList());
            List<String> listStatuts = new List<string>();

            var valuesE = Enum.GetValues(typeof(ReunionStatus)).Cast<ReunionStatus>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listStatuts.Add(attributes[0].Description.ToString().Substring(attributes[0].Description.ToString().IndexOf(":")+1));

            }

            List<String> listAnnee = new List<string>();
            // alimenter les listes 
            var valuesE2 = Enum.GetValues(typeof(Annee)).Cast<Annee>();
            foreach (var r in valuesE2)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            ViewBag.StatutsReunion = new SelectList(listStatuts);
            ViewBag.commission = new SelectList(commissionService.GetAll(), "Id", "Titre");
            ViewBag.StatutsReunionM = new SelectList(listStatuts);
            ViewBag.commissionM = new SelectList(commissionService.GetAll(), "Id", "Titre");
            ViewBag.Annee = new SelectList(listAnnee);
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description");
            
            return View(tuple); 
        }


        public JsonResult GetCommissionEvents(double start, double end, string commission, string annee, string grade, string examType)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);
            var commissionId = 0;
            var gradeId = 0;

            int.TryParse(commission, out commissionId);
            int.TryParse(grade, out gradeId);


            var rslt = reunionService.GetListReunion(fromDate, toDate, commissionId, gradeId, annee, examType);

            var eventList = from e in rslt
                            select new
                            {
                                id = e.ID,
                                comId = e.CommissionId,
                                title = e.Title,
                                start = e.StartDateString,
                                startAffiche = Convert.ToDateTime(e.StartDateString).ToString("d"),
                                dureeS=e.DureeString,
                                heure = e.Heure,
                                duree = e.Duree,
                                end = e.EndDateString,
                                color = e.StatusColor,
                                className = e.ClassName,
                                allDay = false
                            };

            var rows = eventList.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
           
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public ActionResult EnregistrerEvent(Tuple<ProgrammeModel, EventDto> param, string commission, string StatutsReunion)
        {
            Reunion reunion = new Reunion();
            DateTime dateReunion = Convert.ToDateTime(param.Item2.StartDateString);
            string heure = param.Item2.Heure;
            TimeSpan ts = new TimeSpan(Convert.ToInt32(heure.Substring(0, 2)), Convert.ToInt32(heure.Substring(3, 2)), 0);
            
            reunion.DateReunion = dateReunion + ts;
            reunion.DureeString = param.Item2.Duree;
            int heureMinute = Convert.ToInt32(param.Item2.Duree.Substring(0,2)) * 60;
            reunion.Duree = heureMinute + Convert.ToInt32(param.Item2.Duree.Substring(3, 2));
            reunion.OrdreJour = param.Item2.Title;
            reunion.StatusENUM = Enums.GetEnumName<ReunionStatus>(StatutsReunion);
            reunion.Commission = commissionService.GetById(Convert.ToInt32(commission));
            reunionService.Create(reunion); 
            return RedirectToAction("CalendrierCap"); 
        }

        public ActionResult ModifierEvent(Tuple<ProgrammeModel, EventDto> param, string eventIdM, string commissionM, string StatutsReunionM)
        {
            Reunion reunion = reunionService.GetById(Convert.ToInt32(eventIdM));
            DateTime dateReunion = Convert.ToDateTime(param.Item2.StartDateString);
            string heure = param.Item2.Heure;
            TimeSpan ts = new TimeSpan(Convert.ToInt32(heure.Substring(0, 2)), Convert.ToInt32(heure.Substring(3, 2)), 0);

            reunion.DateReunion = dateReunion + ts;
            reunion.DureeString = param.Item2.Duree;
            int heureMinute = Convert.ToInt32(param.Item2.Duree.Substring(0, 2)) * 60;
            reunion.Duree = heureMinute + Convert.ToInt32(param.Item2.Duree.Substring(3, 2));
            reunion.OrdreJour = param.Item2.Title;
            reunion.StatusENUM = Enums.GetEnumName<ReunionStatus>(StatutsReunionM);
            reunion.Commission = commissionService.GetById(Convert.ToInt32(commissionM));
            reunionService.Update(reunion);
            return RedirectToAction("CalendrierCap");
        }

        // Fonction permettant de générer une convocation 

        public ActionResult GenererConvocation(string membreId, string reunionId)
        {
            int idCom = Convert.ToInt32(Session["commissionId"]);
            var convocationUriString = Url.Action("Convocation", "GestionCommission", new { id = membreId, idReunion = reunionId, idCom = idCom }, protocol: Request.Url.Scheme);
            
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


        public ActionResult Convocation(string id, string idReunion, int idCom)
        {
            MembreCommission m = membreCommissionService.GetById(Convert.ToInt32(id));
            Commission c = commissionService.GetById(idCom);
            Reunion reunion = reunionService.GetById(Convert.ToInt32(idReunion));
           //a revoir
            Grade grade = commissionService.GetById(idCom).Grade;
            List<Grade> grades = new List<Grade>();
            grades.Add(grade);
            ViewBag.date = reunion.DateReunion.ToString("D", CultureInfo.CreateSpecificCulture("ar-AE")).Replace(",","");
            ViewBag.timeToString = TimeToLetter(reunion.DateReunion);
            return View(new Tuple<MembreCommission, Commission, Reunion, IEnumerable<Grade>>(m, c, reunion, grades));
        }

        public string TimeToLetter (DateTime date)
        {
            int hour = date.Hour;
            int minute = date.Minute;
            string wordH="";
            string periode="";
            string minuteW="";
            string stringToReturn = ""; 

            switch (hour)
            {
                case 0: wordH = "منتصف الليل"; break;
                case 1:
                case 13: wordH = "الواحدة"; break;
                case 2:
                case 14: wordH = "الثانية"; break;
                case 3:
                case 15: wordH = "الثالثة"; break;
                case 4:
                case 16: wordH = "الرابعة"; break;
                case 5:
                case 17: wordH = "الخامسة"; break;
                case 6:
                case 18: wordH = "السادسة"; break;
                case 7:
                case 19: wordH = "السابعة"; break;
                case 8:
                case 20: wordH = "الثامنة"; break;
                case 9:
                case 21: wordH = "التاسعة"; break;
                case 10:
                case 22: wordH = "العاشرة"; break;
                case 11:
                case 23: wordH = "الحادية عشرة"; break;
                case 12: wordH = "الثانية عشرة"; break;
                default: break;
            }

            if(hour >= 1 && hour < 12)
            {
                periode = "صباحا ";
            } else if(hour >= 12 && hour <= 15)
            {
                periode = "زوالا ";
            }
            else
            {
                periode = "مساءا ";
            }

            if(minute == 15)
            {
                minuteW = "و الربع ";
            }else if(minute == 30)
            {
                minuteW = "و النصف ";
            }else if( minute == 0)
            {
                minuteW = "";
            }
            if (minute != 15 && minute != 30 && minute !=0)
            { 
            minuteW = InternationalNumericFormatter.FormatWithCulture("L", minute, null, new CultureInfo("ar-AE"));
            }

            stringToReturn = " "+wordH + "" + minuteW + "" + periode + "" ;
            return stringToReturn; 
        }

        public ActionResult EnvoyerEmail(ViewModel model, int reunionId, string membreId)
        {
            var file = (byte[])Session["file"]; 
            if(file == null)
            {
                int idCom = Convert.ToInt32(Session["commissionId"]);
                var convocationUriString = Url.Action("Convocation", "GestionCommission", new { id = membreId, idReunion = reunionId, idCom = idCom }, protocol: Request.Url.Scheme);

                file = ConvertHtmlToPdf(convocationUriString);
            }

            try
            {
                Attachment att = new Attachment(new MemoryStream(file), "Convocation");
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);

                mail.From = new MailAddress(ConfigurationManager.AppSettings["SmtpAdress"]);
                mail.To.Add(model.MembreCommission.Email);
                mail.Subject = model.Convocation.Objet;
                mail.Body = model.Convocation.Message;
                mail.Attachments.Add(att);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpAdress"], ConfigurationManager.AppSettings["SmtpPassWord"]);
                SmtpServer.EnableSsl = true; 
                SmtpServer.Send(mail);
                
                
            }
            catch (Exception ex)
            {
                
            }

            return RedirectToAction("ParametrageCap", new { onglet = "membre"});
        }

        // Fonction permettant de générer un Rapport 

      public FileContentResult GenerateAndDisplayReport(int commissionId, string format)
       {
           LocalReport localReport = new LocalReport();
           localReport.ReportPath = Server.MapPath("~/Rapport/Report_AGC5.rdlc");


           //ReportDataSource reportDataSource = new ReportDataSource("DataSet2", customerfilterList);
            //reportDataSource.Name = "DataSet2";

           var customerfilterList = commissionMembreService.GetAll().Where(m => m.Commission_Id == commissionId).Select(m => m.MembreCommission);

                
                //reportDataSource.Value = customerfilterList;

                ReportDataSource reportDataSource = new ReportDataSource("DataSet2", customerfilterList);
            localReport.DataSources.Add(reportDataSource);            
            string reportType = "pdf";            
            string mimeType;            
            string encoding;            
            string fileNameExtension;             
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo =            "<DeviceInfo>" +           
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
            else {
                return File(renderedBytes, "image/jpeg");
            }
       }   
    }
	
}
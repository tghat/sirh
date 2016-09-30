using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.International.Formatters;
using Microsoft.Reporting.WebForms;
using System.IO;
namespace ma.metl.sirh.Controllers
{
    public class DeliberationController : Controller
    {
        IGradeService gradeService;
        ICandidatService candidatService;
        IDetailAvancementService detailAvancementService;
        IExamenService examenService;


        public DeliberationController(IGradeService gradeService, ICandidatService candidatService, IDetailAvancementService detailAvancementService, IExamenService examenService)
        {
            this.gradeService = gradeService;
            this.candidatService = candidatService;
            this.detailAvancementService = detailAvancementService;
            this.examenService = examenService; 
        }

      
        public ActionResult AvChoixDeliberation(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto> tuple)
        {
            List<CandidatResultatDto> listDetail = new List<CandidatResultatDto>(); 
            if(tuple.Item2.GradeId != 0 && tuple.Item2.AnneeProm !="")
            {
                int ordre = 0; 
                List<DetailAvancement> detail = detailAvancementService.GetByGradeAnneeAC(tuple.Item2.GradeId, tuple.Item2.AnneeProm).OrderByDescending(x=>x.Note).ToList();
                foreach(DetailAvancement d in detail)
                {
                    CandidatResultatDto candidat = new CandidatResultatDto();
                    candidat.Nom = d.Candidat.Nom;
                    candidat.Prenom = d.Candidat.Prenom;
                    candidat.NoteMoyenne = d.Note;
                    ordre=ordre+1;
                    candidat.ordreMerite = ordre;
                    listDetail.Add(candidat);
                }
                listDetail.OrderByDescending(x => x.ordreMerite);
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
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description", tuple.Item2.GradeId);
            ViewBag.Examens = new SelectList(examenService.GetAll(), "Id", "Description", tuple.Item2.GradeId);
            ViewBag.Annee = new SelectList(listAnnee, tuple.Item2.AnneeProm);
            tuple = new Tuple<IEnumerable<CandidatResultatDto>,CandidatCritereRechDto>(listDetail,tuple.Item2);
            return View(tuple);
        }


        public ActionResult AvExamenDeliberation(Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto> tuple)
        {
            List<CandidatResultatDto> listDetail = new List<CandidatResultatDto>();
            List<Examen> listExamens = new List<Examen>();
            if (tuple.Item2.AnneeProm != null && tuple.Item2.GradeId != 0 && tuple.Item2.ExamenId != 0 && !tuple.Item2.Resultat.Equals("Selectionnez") && !tuple.Item2.EtatProm.Equals("Selectionnez"))
            {
                int ordre = 0;
                
                if (tuple.Item2.Resultat.Equals(Resultats.ResEcrit))
                {
                    List<CandidatResultatDto> listCandidat = detailAvancementService.GetByGradeAnneeAEEcrit(tuple.Item2.GradeId, tuple.Item2.AnneeProm).OrderByDescending(x => x.NoteEcrite).ToList();
                    foreach (CandidatResultatDto d in listCandidat)
                    {
                        ordre = ordre + 1;
                        d.ordreMerite = ordre;
                        listDetail.Add(d);
                    }
                }

                if (tuple.Item2.Resultat.Equals(Resultats.ResOral))
                {
                    List<CandidatResultatDto> listCandidat = detailAvancementService.GetByGradeAnneeAEOral(tuple.Item2.GradeId, tuple.Item2.AnneeProm).OrderByDescending(x => x.NoteOrale).ToList();
                    foreach (CandidatResultatDto d in listCandidat)
                    {
                        ordre = ordre + 1;
                        d.ordreMerite = ordre;
                        listDetail.Add(d);
                    }
                }

                if (tuple.Item2.Resultat.Equals(Resultats.ResDef))
                {
                    List<CandidatResultatDto> listCandidat = detailAvancementService.GetByGradeAnneeAEGlobal(tuple.Item2.GradeId, tuple.Item2.AnneeProm).OrderByDescending(x => x.NoteGlobale).ToList();
                    foreach (CandidatResultatDto d in listCandidat)
                    {
                        ordre = ordre + 1;
                        d.ordreMerite = ordre;
                        listDetail.Add(d);
                    }
                }
                listDetail.OrderByDescending(x => x.ordreMerite);
            }
            if (tuple.Item2.AnneeProm != null && tuple.Item2.GradeId != 0)
            {
                int idGrade = Convert.ToInt32(tuple.Item2.GradeId);
                listExamens = examenService.GetAll().Where(x => x.Annee == tuple.Item2.AnneeProm && x.Grade.Id == Convert.ToInt32(tuple.Item2.GradeId)).ToList();
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

            ViewBag.Examens = new SelectList(listExamens, "Id", "Intitule", tuple.Item2.ExamenId);
            ViewBag.Grades = new SelectList(gradeService.GetAll().OrderBy(x => x.Description), "Id", "Description", tuple.Item2.GradeId);
            ViewBag.Annee = new SelectList(listAnnee, tuple.Item2.AnneeProm);
            tuple = new Tuple<IEnumerable<CandidatResultatDto>, CandidatCritereRechDto>(listDetail, tuple.Item2);
            return View(tuple);
        }


        public FileStreamResult imprimerAvcChoixDeliberation()
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Rapport/AvcChoixDeliberation.rdlc");


            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "AvcChoixDeliberation";

            //ParametrageQuota param = parametrageQuotaService.GetById(Convert.ToInt32(quotaId));
            //Examen exam = examenService.GetByAnneeAndIdGrade(param.Annee, Convert.ToInt32(param.GradeAcces.Id));
            //QuotaDto quotaDto = parametrageQuotaService.GetNbrPosteAC(param.Annee, Convert.ToInt32(param.GradeAcces.Id));

            List<DeliberationRapportDto> paramList = new List<DeliberationRapportDto>();

            DeliberationRapportDto parametre = new DeliberationRapportDto();
            parametre.OrdreMerite = "test";
            parametre.Nom = "test";
            parametre.Prenom = "test";
                      
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
            Response.AddHeader("content-disposition", "attachment;filename=AGE_n17.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            return new FileStreamResult(ms, "application/pdf");

        }


	}
}
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
    public class PublicationController : Controller
    {
        IPublicationService publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            this.publicationService = publicationService; 
        }
        //
        // GET: /Publication/
        public ActionResult PublicationAve(Tuple<ProgrammeModel, Publication, IEnumerable<Publication>> prog, string source)
        {
            List<Publication> listPub = new List<Publication>(); 
            if(source == "creation")
                ModelState.AddModelError("msgInfo", "La publication est créée avec succès!");
            if (prog.Item1.TypePublication == TypePublication.Selectionnez && prog.Item1.Etat == Etat.Selectionnez && prog.Item1.DateDebut == null && prog.Item1.DateFin == null)
            {
                listPub = publicationService.GetAllByType("AVE").ToList(); 
            }
            else
            {
                listPub = publicationService.GetPublicationByCriteres(prog.Item1,"AVE");
            }
            foreach (Publication pub in listPub)
            {
                Array values = Enum.GetValues(typeof(TypePublication));
                foreach (var value in values)
                {
                    if (pub.TypePublication.Equals(value.ToString()))
                    {
                        pub.TypePublication = GetEnumDescription(value);
                    }
                }

            }

            List<ListEnum> listTypePub = new List<ListEnum>();
            var valuesE = Enum.GetValues(typeof(TypePublication)).Cast<TypePublication>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                ListEnum element = new ListEnum();
                element.Code = r.ToString();
                element.Description = attributes[0].Description.ToString();
                listTypePub.Add(element);

            }

            List<ListEnum> listTypeEtat = new List<ListEnum>();
            var valuesT = Enum.GetValues(typeof(EtatPublication)).Cast<EtatPublication>();
            foreach (var r in valuesT)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                ListEnum element = new ListEnum();
                element.Code = r.ToString();
                element.Description = attributes[0].Description.ToString();
                listTypeEtat.Add(element);

            }
            ViewBag.TypePublication = new SelectList(listTypePub, "Code", "Description", prog.Item1.TypePublication);
            ViewBag.EtatPublication = new SelectList(listTypeEtat, "Code", "Description", prog.Item1.EtatPublication);
            var tuple = new Tuple<ProgrammeModel, Publication, IEnumerable<Publication>>(prog.Item1,new Publication(), listPub);
            return View(tuple);
        }

        // GET: /Publication/
        public ActionResult PublicationAvc(Tuple<ProgrammeModel, Publication, IEnumerable<Publication>> prog, string source)
        {

            List<Publication> listPub = new List<Publication>();
            if (source == "creation")
                ModelState.AddModelError("msgInfo", "La publication est créée avec succès!");
            if (prog.Item1.TypePublicationAVC == TypePublicationAVC.Selectionnez && prog.Item1.Etat == Etat.Selectionnez && prog.Item1.DateDebut == null && prog.Item1.DateFin == null)
            {
                listPub = publicationService.GetAllByType("AVC").ToList();
            }
            else
            {
                listPub = publicationService.GetPublicationByCriteres(prog.Item1, "AVC");
            }
            foreach (Publication pub in listPub)
            {
                Array values = Enum.GetValues(typeof(TypePublicationAVC));
                foreach (var value in values)
                {
                    if (pub.TypePublication.Equals(value.ToString()))
                    {
                        pub.TypePublication = GetEnumDescription(value);
                    }
                }

            }

            List<ListEnum> listTypePub = new List<ListEnum>();
            var valuesE = Enum.GetValues(typeof(TypePublicationAVC)).Cast<TypePublicationAVC>();
            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                ListEnum element = new ListEnum();
                element.Code = r.ToString();
                element.Description = attributes[0].Description.ToString();
                listTypePub.Add(element);

            }

            List<ListEnum> listTypeEtat = new List<ListEnum>();
            var valuesT = Enum.GetValues(typeof(EtatPublication)).Cast<EtatPublication>();
            foreach (var r in valuesT)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                ListEnum element = new ListEnum();
                element.Code = r.ToString();
                element.Description = attributes[0].Description.ToString();
                listTypeEtat.Add(element);

            }
            ViewBag.TypePublicationAVC = new SelectList(listTypePub, "Code", "Description", prog.Item1.TypePublicationAVC);
            ViewBag.EtatPublication = new SelectList(listTypeEtat, "Code", "Description", prog.Item1.EtatPublication);
            var tuple = new Tuple<ProgrammeModel, Publication, IEnumerable<Publication>>(prog.Item1, new Publication(), listPub);
            return View(tuple);
        }

        public ActionResult CreerPublication(Tuple<ProgrammeModel, Publication, IEnumerable<Publication>> prog)
        {
            Publication publication = new Publication();
            publication.TypePublication = prog.Item2.TypePublication; 
            publication.Objet = prog.Item2.Objet;
            publication.DateDebutPub = prog.Item2.DateDebutPub;
            publication.DateFinPub = prog.Item2.DateFinPub;
            publication.Statut = prog.Item2.Statut;
            HttpPostedFileBase file = Request.Files["flux"];
               if (file != null && file.ContentLength > 0)
                {
                    byte[] content = new byte[file.ContentLength];
                    file.InputStream.Read(content, 0, file.ContentLength);
                    publication.PieceJointePub = content;
                }
            publication.ContentType = file.ContentType;
            publication.FileName = file.FileName;
            publication.Type = "AVE"; 
            publicationService.Create(publication);

            return RedirectToAction("PublicationAve", new {source="creation" });
        }

        public ActionResult CreerPublicationAVC(Tuple<ProgrammeModel, Publication, IEnumerable<Publication>> prog)
        {
            Publication publication = new Publication();
            publication.TypePublication = prog.Item2.TypePublication;
            publication.Objet = prog.Item2.Objet;
            publication.DateDebutPub = prog.Item2.DateDebutPub;
            publication.DateFinPub = prog.Item2.DateFinPub;
            publication.Statut = prog.Item2.Statut;
            HttpPostedFileBase file = Request.Files["flux"];
            if (file != null && file.ContentLength > 0)
            {
                byte[] content = new byte[file.ContentLength];
                file.InputStream.Read(content, 0, file.ContentLength);
                publication.PieceJointePub = content;
            }
            publication.ContentType = file.ContentType;
            publication.FileName = file.FileName;
            publication.Type = "AVC";
            publicationService.Create(publication);

            return RedirectToAction("PublicationAvc", new { source = "creation" });
        }

        private string GetEnumDescription<TEnum>(TEnum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        /*Fonction permettant de télécharger une publication*/

        public void TelechargerFichier(string FileId)
        {
            byte[] bytes;
            string fileName, contentType;
            Publication publication = publicationService.GetById(Convert.ToInt32(FileId));
            bytes = publication.PieceJointePub;
            contentType = publication.ContentType;
            fileName = publication.FileName; 
           
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
	}
}
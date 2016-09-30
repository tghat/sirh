using ma.metl.sirh.Common.TraceHistory;
using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Core;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using System.Xml;
using System.Data.Odbc;


namespace ma.metl.sirh.Controllers
{
    public class IntegrationFileController : BaseController
    {
        IFluxService fluxService;
        ILigneRejeteeService ligneRejeteeService;
        IDirectionService directionService;
        ICandidatService candidatService;
        ICandidatGOService candidatGOService;
        IDetailAvancementService detailAvancementService;
        IGradeService gradeService;
        ILocaliteService localiteService;
        IMvtSitAdmService mvtSitAdmService;
        ISanctionService sanctionService;

        public IntegrationFileController(IFluxService fluxService, ILigneRejeteeService ligneRejeteeService, IDirectionService directionService, ICandidatService candidatService, IDetailAvancementService detailAvancementService, IGradeService gradeService, ILocaliteService localiteService, ICandidatGOService candidatGOService, IMvtSitAdmService mvtSitAdmService, ISanctionService sanctionService, IHistoriqueService historiqueService)
            : base(historiqueService, detailAvancementService)
        {
            this.fluxService = fluxService;
            this.ligneRejeteeService = ligneRejeteeService;
            this.directionService = directionService;
            this.candidatService = candidatService;
            this.detailAvancementService = detailAvancementService;
            this.gradeService = gradeService;
            this.localiteService = localiteService;
            this.candidatGOService = candidatGOService;
            this.mvtSitAdmService = mvtSitAdmService;
            this.sanctionService = sanctionService;
        }

        //
        // GET: /IntegrationFile/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IntegrationFE()
        {
            ////alimenter les listes 
            List<String> listAnnee = new List<string>();

            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();

            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            ViewBag.Annee = new SelectList(listAnnee);

            List<Flux> liste = new List<Flux>();
            ProgrammeModel objprogrammemodel = new ProgrammeModel();
            var tuple = new Tuple<ProgrammeModel, IEnumerable<Flux>>(objprogrammemodel, liste);
            ViewBag.show = false;
            return View(tuple);
        }

        [HttpPost]
        public ActionResult IntegrationFE(Tuple<ProgrammeModel, IEnumerable<Flux>> p, string IdFlux)
        {
            List<Flux> liste = new List<Flux>();
            ProgrammeModel programme = new ProgrammeModel();
            ViewBag.show = false;

            liste = fluxService.GetAll().ToList();

            // Effectuer la recherche selon les critères de recherche spécifié 

            if (!p.Item1.Etat.ToString().Equals("Selectionnez"))
            {
                liste = fluxService.GetAll().Where(x => (x.Etat.Equals(p.Item1.Etat.ToString()))).ToList();
            }
            if (!p.Item1.TypeFlux.ToString().Equals("Selectionnez"))
            {
                liste = liste.Where(x => (x.TypeFlux.Equals(p.Item1.TypeFlux.ToString()))).ToList();
            }
            if (p.Item1 != null && p.Item1.Annee!= null && !p.Item1.Annee.ToString().Equals("Sélectionnez"))
            {
                liste = liste.Where(x => (x.anneeReception.Equals(p.Item1.Annee.ToString()))).ToList();
            }

            foreach (Flux flux in liste)
            {
                Array values = Enum.GetValues(typeof(Etat));
                foreach (var value in values)
                {
                    if (flux.Etat.Equals(value.ToString()))
                    {
                        flux.Etat = GetEnumDescription(value);
                    }
                }
                values = Enum.GetValues(typeof(TypeFlux));
                foreach (var value in values)
                {
                    if (flux.TypeFlux.Equals(value.ToString()))
                    {
                        flux.TypeFlux = GetEnumDescription(value);
                    }
                }

            }

            if (IdFlux != null && IdFlux != "")
            {
                long id = Convert.ToInt32(IdFlux);

                Flux flux = fluxService.GetById(Convert.ToInt32(IdFlux));
                ViewBag.nbrTotalLigne = flux.nbrTotalLigne;
                ViewBag.nbrLigneTraite = flux.nbrTotalLigne - flux.nbrLigneRejete;
                ViewBag.nbrLigneRejete = flux.nbrLigneRejete;
                ViewBag.show = true;
                ViewBag.Id = Convert.ToInt32(IdFlux);
            }
            List<String> listAnnee = new List<string>();

            var valuesE = Enum.GetValues(typeof(Annee)).Cast<Annee>();

            foreach (var r in valuesE)
            {
                var field = r.GetType().GetField(r.ToString());
                var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listAnnee.Add(attributes[0].Description.ToString());

            }
            ViewBag.Annee = new SelectList(listAnnee, p.Item1.Annee);
            var tuple = new Tuple<ProgrammeModel, IEnumerable<Flux>>(programme, liste);
            return View(tuple);
        }

        private string GetEnumDescription<TEnum>(TEnum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }


        public ActionResult ConsulterSynthese(Tuple<ProgrammeModel, IEnumerable<Flux>> p, string FileId)
        {
            long id = Convert.ToInt32(FileId);

            Flux flux = fluxService.GetById(Convert.ToInt32(FileId));
            ViewBag.nbrTotalLigne = flux.nbrTotalLigne;
            ViewBag.nbrLigneTraite = flux.nbrTotalLigne - flux.nbrLigneRejete;
            ViewBag.nbrLigneRejete = flux.nbrLigneRejete;
            ViewBag.show = true;
            ViewBag.Id = Convert.ToInt32(FileId);
            // JavaScript("window.popup('synthese');");

            return RedirectToAction("IntegrationFE", "IntegrationFile", new { FileId = id });

        }


        public void TelechargerFichier(string FileId)
        {

            long id = Convert.ToInt32(FileId);
            Flux flux = fluxService.GetAll().Where(x => x.Id == id).First();
            string pathfile = flux.flux + flux.name;

            FileInfo fileInfo = new FileInfo(pathfile);

            if (fileInfo.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + pathfile);
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.ContentType = "application/xml";
                Response.Flush();
                Response.TransmitFile(pathfile);

            }

        }
        [LogTraceAttribute(Action = "Validation")]
        public ActionResult ValiderFichier(string FileId)
        {
            // list des id pour historisation 
            List<long> listId = new List<long>();

            // mettre à jour le statut du fichier
            long id = Convert.ToInt32(FileId);
            Flux flux = fluxService.GetAll().Where(x => x.Id == id).First();
            List<DetailAvancement> list = flux.ListDetailAvancement;
            foreach (DetailAvancement detail in list)
            {
                listId.Add(detail.Id); 
            }
            ViewBag.listId = listId;
            flux.Etat = Etat.Valide.ToString();
            fluxService.Update(flux);

            return RedirectToAction("IntegrationFE");
        }

        public ActionResult IntegrationM()
        {
            return View();
        }

        public ActionResult FermerPopUp()
        {
            ViewBag.show = false;
            return RedirectToAction("IntegrationFE");
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

        //protected CandidatGODto GetCandidatFromGipeOrd(string selectStatement, OdbcConnection connection)
        //{

        //    CandidatGODto candidat = new CandidatGODto(); ;

        //    // Create a dataset
        //    System.Data.DataSet dataSet = new System.Data.DataSet();
        //    OdbcDataAdapter dataAdapter = new OdbcDataAdapter();
        //    OdbcCommandBuilder commandBuilder = new OdbcCommandBuilder(dataAdapter);
        //    dataAdapter.SelectCommand = new OdbcCommand(selectStatement, connection);
        //    dataAdapter.Fill(dataSet);

        //    // Iterate through all the rows   
        //    var test = dataSet.Tables;
        //    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
        //    {
        //        foreach (System.Data.DataColumn column in dataSet.Tables[0].Columns)
        //        {
        //            Console.WriteLine(dataSet.Tables[0].Rows[i][column.ColumnName]);
        //            string columnName = column.ColumnName;
        //            switch (columnName)
        //            {
        //                case "COD_AG":
        //                    candidat.NumDoti = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : dataSet.Tables[0].Rows[i][column.ColumnName].ToString();
        //                    break;
        //                case "NOM_AG":
        //                    candidat.Nom = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "PRENOM_AG":
        //                    candidat.Prenom = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "NOM_ARAB_AG":
        //                    candidat.NomAR = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "PRENOM_ARAB_AG":
        //                    candidat.PrenomAR = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "DAT_REC":
        //                    candidat.DateRecrutement = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? null : (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "ANN_N_AG":
        //                    candidat.AnnDateNaissance = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? short.Parse("0") : short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
        //                    break;
        //                case "MOI_N_AG":
        //                    candidat.MoiDateNaissance = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? short.Parse("0") : short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
        //                    break;
        //                case "JOU_N_AG":
        //                    candidat.JourDateNaissance = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? short.Parse("0") : short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
        //                    break;
        //                case "SEX_AG":
        //                    candidat.Sexe = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "COD_LOC":
        //                    candidat.Localite = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? short.Parse("0") : short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
        //                    break;
        //                case "ANC_GRADE":
        //                    candidat.AncGrade = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? null : (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "ANC_ELO":
        //                    candidat.AncEchelon = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? null : (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "COD_S_AF_FTF":
        //                    candidat.Direction = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "CIN_A_AG":
        //                    candidat.CINA = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "CIN_N_AG":
        //                    candidat.CINN = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? int.Parse("0") : int.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
        //                    break;
        //                case "COD_GRADE":
        //                    candidat.CodeGrade = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "COD_CATEG":
        //                    candidat.CodeCateg = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "COD_CORPS":
        //                    candidat.CodeCorps = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //                case "COD_CADRE":
        //                    candidat.CodeCadre = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : (string)dataSet.Tables[0].Rows[i][column.ColumnName];
        //                    break;
        //            }
        //        }
        //    }

        //    if (String.IsNullOrEmpty(candidat.NumDoti))
        //    {
        //        candidat = null;
        //    }
        //    return candidat;
        //}

        protected List<SanctionDto> GetListSanctions(string selectStatement, OdbcConnection connection)
        {

            List<SanctionDto> list = new List<SanctionDto>();
            // Create a dataset
            System.Data.DataSet dataSet = new System.Data.DataSet();
            OdbcDataAdapter dataAdapter = new OdbcDataAdapter();
            OdbcCommandBuilder commandBuilder = new OdbcCommandBuilder(dataAdapter);
            dataAdapter.SelectCommand = new OdbcCommand(selectStatement, connection);
            dataAdapter.Fill(dataSet);

            // Iterate through all the rows   
            var test = dataSet.Tables;
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                SanctionDto dto = new SanctionDto();
                foreach (System.Data.DataColumn column in dataSet.Tables[0].Columns)
                {
                    
                    Console.WriteLine(dataSet.Tables[0].Rows[i][column.ColumnName]);
                    string columnName = column.ColumnName;
                    switch (columnName)
                    {
                        case "LIB_SS_TYP_MVT":
                            dto.Titre = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? "" : dataSet.Tables[0].Rows[i][column.ColumnName].ToString();
                            break;
                        case "M_REF_ARRETE":
                            dto.Reference = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? int.Parse("0") : int.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
                            break;
                        case "M_DAT_EFF_MVT":
                            dto.DateEffet = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? null : (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
                            break;
                        case "M_ANN_VISA":
                            dto.AnneeActe = dataSet.Tables[0].Rows[i][column.ColumnName] == DBNull.Value ? short.Parse("0") : short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
                            break;                       
                    }

                  
                }
                list.Add(dto);
            }
            return list;
        } 

        [HttpPost]
        [LogTraceAttribute(Action = "Importation depuis GipeOrd")]
        public ActionResult IntegrationM(string TypeFlux)
        {
            // récupérer le fichier à partir du view 
            HttpPostedFileBase file = Request.Files["flux"];
            List<long> listId = new List<long>();

            if(TypeFlux == "Selectionnez" || file.ContentLength == 0)
            {
                ModelState.AddModelError("msgError", "Veuillez renseigner les champs type flux et fichier!");
                return View(); 
            }

            if (file.ContentType != "text/xml")
            {
                ModelState.AddModelError("msgError", "Seuls les fichiers xml sont supportés!");
                return View();
            }
            // list des id pour historisation 
           

            // récupérer le répertoire 
            string directoryInfo = ConfigurationManager.AppSettings["DirectoryInfo"];
            //renomer le fichier uploader 
            string destFilePathOk = @directoryInfo + "\\Inbox\\";
            string name = "GipeOrd_" + TypeFlux + "_" + DateTime.Now.Day.ToString("00") + "" + DateTime.Now.Month.ToString("00") + "" + DateTime.Now.Year + ".xml";
            string cheminComplet = destFilePathOk + name;
            file.SaveAs(cheminComplet);

            //Lecture du fichier

            XmlDocument xmlDoc = new XmlDocument();
            // charge le fichier XML
            xmlDoc.Load(cheminComplet);

            // retourne le nombre de lignes du fichier
            int nbrLigneFichier = xmlDoc.SelectNodes("PROMOGRADE/LIST_G_COD_AG/G_COD_AG").Count;

            //lecture du fichier 

            XmlNodeList promoGrade = xmlDoc.SelectNodes("/PROMOGRADE/LIST_G_COD_AG");
            XmlNodeList list = xmlDoc.GetElementsByTagName("G_COD_AG");
            List<LigneRejetee> lignesRejetees = new List<LigneRejetee>();
            List<Candidat> listeCandidat = new List<Candidat>();
            List<DetailAvancement> listDetail = new List<DetailAvancement>();

            // Specify Data Source Name DSN along with user and password

            //OdbcConnection connection = GetConnection("DSN=Connexion11g;UID=GIPEORD;PWD=GipeOrd");
            OdbcConnection connection = GetConnection("DSN=Oracle9iConnexion;UID=Gipeord_sirh_test;PWD=test");


            foreach (XmlNode node in list)
            {
                DetailAvancement detail = new DetailAvancement();
                CandidatGODto candidatGO = new CandidatGODto(); 
                bool rejet = false;
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    string nameChildNode = node.ChildNodes[i].Name;
                    string contenuChildNode = node.ChildNodes[i].InnerText;
                    List<DetailAvancement> listD = detailAvancementService.GetDetailByNumDoti(contenuChildNode);
                    if (nameChildNode == "COD_AG")
                    {
                        string numDoti = contenuChildNode;
                       
                        candidatGO = candidatGOService.GetCandidatFromGipeOrd(connection, numDoti);                        
                    
                        if(candidatGO == null)
                        {
                            LigneRejetee ligneRejetee = new LigneRejetee();
                            ligneRejetee.detail = node.ToString();
                            ligneRejetee.motifRejet = "Le numDoti en question n'existe pas dans la base de données GipeOrd!";
                            lignesRejetees.Add(ligneRejetee);
                            rejet = true;
                            break;
                        }
                        if (listD != null && listD.Count > 0)
                        {
                            listD = listD.Where(x => !x.Statut.Equals(Etat.Valide.ToString())).ToList();
                            if (listD.Count != 0)
                            {
                                LigneRejetee ligneRejetee = new LigneRejetee();
                                ligneRejetee.detail = node.ToString();
                                ligneRejetee.motifRejet = "Le candidat en question a déjà un avancement en cours!";
                                lignesRejetees.Add(ligneRejetee);
                                rejet = true;
                                break;
                            }
                        }
                    }
                    if (nameChildNode == "CF_N_GRADE")
                    {
                        if (listD != null && listD.Count > 0)
                        {
                            listD = listD.Where(x => x.NouveauGrade.Equals(contenuChildNode)).ToList();
                            if (listD.Count != 0)
                            {
                                LigneRejetee ligneRejetee = new LigneRejetee();
                                ligneRejetee.detail = node.ToString();
                                ligneRejetee.motifRejet = "Le candidat en question a déjà un avancement identique!";
                                lignesRejetees.Add(ligneRejetee);
                                rejet = true;
                                break;
                            }
                        }
                    }
                    if (nameChildNode == "ANC_GRADE" || nameChildNode == "DATE_EFF_MVT")
                    {
                        try
                        {
                            Convert.ToDateTime(contenuChildNode);
                        }
                        catch (FormatException excp)
                        {
                            Console.WriteLine(excp.GetType().Name);
                            LigneRejetee ligneRejetee = new LigneRejetee();
                            ligneRejetee.detail = node.ToString();
                            ligneRejetee.motifRejet = "Date incorrecte!";
                            lignesRejetees.Add(ligneRejetee);
                            rejet = true;
                            break;
                        }
                    }
                }
                if (rejet == false)
                {
                    string numDoti = node.ChildNodes[0].InnerText;
                    Candidat candidat = candidatService.GetByNumDoti(numDoti);
                    
                    int annee = 0;
                    int mois = 0;
                    int jour = 0;
                    bool insertDoing = false;
                    DateTime ancGrade = new DateTime();
                    DateTime ancEchelon = new DateTime(); 
                    if (candidat == null && candidatGO != null)
                    {
                        // à revoir avec le client
                        candidat = new Candidat();
                        candidat.NumDoti = candidatGO.NumDoti;
                        candidat.Nom = candidatGO.Nom;
                        candidat.NomAR = candidatGO.NomAR;
                        candidat.Prenom = candidatGO.Prenom;
                        candidat.PrenomAR = candidatGO.PrenomAR;
                        candidat.Sexe = candidatGO.Sexe;
                        candidat.DateRecrutement = candidatGO.DateRecrutement;
                        candidat.DateEffet = Convert.ToDateTime(node.ChildNodes[10].InnerText);
                        candidat.Grade = gradeService.GetGradeByCode(node.ChildNodes[1].InnerText, node.ChildNodes[2].InnerText, node.ChildNodes[3].InnerText, node.ChildNodes[4].InnerText);
                        candidat.CIN = candidatGO.CINA + "" + candidatGO.CINN.ToString();
                        candidat.Direction = directionService.GetByCode(candidatGO.Direction);
                        ancGrade = Convert.ToDateTime(candidatGO.AncGrade);
                        ancEchelon = Convert.ToDateTime(candidatGO.AncEchelon);
                        if (candidatGO.AnnDateNaissance != 0 && candidatGO.AnnDateNaissance != null)
                        {
                            annee = Convert.ToInt32(candidatGO.AnnDateNaissance);
                        }
                        if (candidatGO.MoiDateNaissance != 0 && candidatGO.MoiDateNaissance != null)
                        {
                            mois = Convert.ToInt32(candidatGO.MoiDateNaissance);
                        }
                        if (candidatGO.JourDateNaissance != 0 && candidatGO.JourDateNaissance != null)
                        {
                            jour = Convert.ToInt32(candidatGO.JourDateNaissance);
                        }
                        if (annee != 0  && mois != 0  && jour != 0 )
                        {
                            candidat.DateNaissance = new DateTime(annee, mois, jour);
                        }
                        candidat.Direction = directionService.GetById(1);
                        //candidat.Localite = localiteService.GetById(Convert.ToInt32(candidatGO.Localite));
                        candidat.Localite = localiteService.GetByIdOrd(Convert.ToInt32(candidatGO.Localite));
                        candidatService.Create(candidat);
                        insertDoing = true;

                        string querySanction1 = "SELECT DISTINCT ss.LIB_SS_TYP_MVT, sa.M_REF_ARRETE, sa.M_DAT_EFF_MVT,"+
                                               "sa.M_ANN_VISA from MVT_SIT_ADM sa," +
                                               "TAB_SS_TYP_MVT ss where sa.M_TYP_MVT = ss.TYP_MVT and sa.M_SS_TYP_MVT = ss.SS_TYP_MVT"+
                                               " and sa.M_TYP_MVT = 'SA' and sa.M_COD_AG = "+numDoti+";";

                        List<SanctionDto> listSanctions = GetListSanctions(querySanction1, connection);
                        //List<SanctionDto> listSanctions = mvtSitAdmService.GetListSanctions(numDoti); 

                        if(listSanctions != null)
                        {
                            foreach(SanctionDto s in listSanctions)
                            {
                                Sanction sanction = new Sanction();
                                sanction.Description = s.Titre;
                                sanction.DateEffet = s.DateEffet;
                                sanction.AnneeActe = Convert.ToString(s.AnneeActe);
                                sanction.Reference = s.Reference;
                                sanction.Candidat = candidat;
                                sanctionService.Create(sanction);
                            }
                        }
                    }
                    else if (candidat != null && candidatGO != null)
                    {
                        candidat.Nom = candidatGO.Nom;
                        candidat.NomAR = candidatGO.NomAR;
                        candidat.Prenom = candidatGO.Prenom;
                        candidat.PrenomAR = candidatGO.PrenomAR;
                        candidat.Sexe = candidatGO.Sexe;
                        candidat.DateRecrutement = candidatGO.DateRecrutement;
                        candidat.CIN = candidatGO.CINA + "" + candidatGO.CINN.ToString();
                        candidat.DateEffet = Convert.ToDateTime(node.ChildNodes[10].InnerText);
                        candidat.Grade = gradeService.GetGradeByCode(node.ChildNodes[1].InnerText, node.ChildNodes[2].InnerText, node.ChildNodes[3].InnerText, node.ChildNodes[4].InnerText);
                        if (candidatGO.AnnDateNaissance != 0 && candidatGO.AnnDateNaissance != null)
                        {
                            annee = Convert.ToInt32(candidatGO.AnnDateNaissance);
                        }
                        if (candidatGO.MoiDateNaissance != 0 && candidatGO.MoiDateNaissance != null)
                        {
                            mois = Convert.ToInt32(candidatGO.MoiDateNaissance);
                        }
                        if (candidatGO.JourDateNaissance != 0 && candidatGO.JourDateNaissance != null)
                        {
                            jour = Convert.ToInt32(candidatGO.JourDateNaissance);
                        }
                        if (annee != 0 && mois != 0 && jour != 0)
                        {
                            candidat.DateNaissance = new DateTime(annee, mois, jour);
                        }
                        candidat.Direction = directionService.GetById(1);
                        //candidat.Localite = localiteService.GetById(Convert.ToInt32(candidatGO.Localite));
                        candidat.Localite = localiteService.GetByIdOrd(Convert.ToInt32(candidatGO.Localite));
                        ancGrade = Convert.ToDateTime(candidatGO.AncGrade);
                        ancEchelon = Convert.ToDateTime(candidatGO.AncEchelon);
                        candidatService.Update(candidat);
                        insertDoing = true;
                        List<Sanction> listSanctionSirh = sanctionService.GetListSanctionByNumDoti(numDoti);
                        if(listSanctionSirh != null)
                        {
                            foreach(Sanction s in listSanctionSirh)
                            {
                                sanctionService.Delete(s);
                            }

                            string querySanction2 = "SELECT DISTINCT ss.LIB_SS_TYP_MVT, sa.M_REF_ARRETE, sa.M_DAT_EFF_MVT," +
                                               "sa.M_ANN_VISA from MVT_SIT_ADM sa," +
                                               "TAB_SS_TYP_MVT ss where sa.M_TYP_MVT = ss.TYP_MVT and sa.M_SS_TYP_MVT = ss.SS_TYP_MVT" +
                                               " and sa.M_TYP_MVT = 'SA' and sa.M_COD_AG = " + numDoti + ";";

                            List<SanctionDto> listSanctions = GetListSanctions(querySanction2, connection);
                            //List<SanctionDto> listSanctions = mvtSitAdmService.GetListSanctions(numDoti);

                            if (listSanctions != null)
                            {
                                foreach (SanctionDto s in listSanctions)
                                {
                                    Sanction sanction = new Sanction();
                                    sanction.Description = s.Titre;
                                    sanction.DateEffet = s.DateEffet;
                                    sanction.AnneeActe = Convert.ToString(s.AnneeActe);
                                    sanction.Reference = s.Reference;
                                    sanction.Candidat = candidat;
                                    sanctionService.Create(sanction);
                                }
                            }
                        }
                    }

                    if (insertDoing)
                    {
                        detail.Annee = DateTime.Now.Year.ToString(); 
                        detail.DateAncienGrade = Convert.ToDateTime(node.ChildNodes[9].InnerText);
                        detail.DateEff = Convert.ToDateTime(node.ChildNodes[10].InnerText);
                        detail.AncienGrade = gradeService.GetGradeByCode(node.ChildNodes[1].InnerText, node.ChildNodes[2].InnerText, node.ChildNodes[3].InnerText, node.ChildNodes[4].InnerText);
                        detail.NouveauGrade = gradeService.GetGradeByCode(node.ChildNodes[5].InnerText, node.ChildNodes[6].InnerText, node.ChildNodes[7].InnerText, node.ChildNodes[8].InnerText);
                        detail.Statut = EtatDetailAVC.EnCours.ToString();
                        detail.DecisionCap = EtatDetailAVC.EnCours.ToString();
                        detail.Candidat = candidat;
                        detail.AncienneteGrade = ancGrade;
                        detail.AncienneteEchelon = ancEchelon;
                        listDetail.Add(detail);

                    }
                 }
                }
             connection.Dispose();
             foreach (LigneRejetee ligne in lignesRejetees)
             {
                 ligneRejeteeService.Create(ligne); 
             }

            //insertion des données du fichier  
            Flux flux = new Flux();
            flux.TypeFlux = TypeFlux;
            flux.flux = destFilePathOk;
            flux.name = name;
            flux.anneeReception = DateTime.Now.Year.ToString();
            flux.dateIntegration = Convert.ToDateTime(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
            flux.Etat = Etat.Rejete.ToString();
            flux.Source = "GipeOrd";
            flux.nbrTotalLigne = nbrLigneFichier;
            flux.nbrLigneRejete = lignesRejetees.Count;
            flux.LignesRejetees = lignesRejetees;
            if (lignesRejetees.Count == nbrLigneFichier)
            {
                flux.Etat = Etat.Rejete.ToString();
            }
            else
            {
                flux.Etat = Etat.EnCours.ToString();
            }

            //créer le fichier accusé de réception 

            string pathAccuse = @directoryInfo + "\\Outbox\\" + Path.GetFileNameWithoutExtension(name) + "_AR.txt";
            //TextWriter tw = new StreamWriter(pathAccuse);
            String txt = "Nombre total de lignes est de :" + nbrLigneFichier + "\n" + "Nombre de lignes rejetée est de :" + lignesRejetees.Count + "\n" + "nombre de lignes inserées est de :" + listeCandidat.Count;
            System.IO.File.WriteAllText(pathAccuse, txt);

            // inserer le flux dans la base de données 

            fluxService.Create(flux);

            // create detail avancement 
            foreach (DetailAvancement d in listDetail)
            {
                d.Flux = flux;
                detailAvancementService.Create(d);
                listId.Add(d.Id);
            }
            ViewBag.listId = listId;
            ModelState.AddModelError("msgInfo", "Intégration effectuée avec succès!");
            return View();
        }


    }
}
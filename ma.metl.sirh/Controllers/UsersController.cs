using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Oracle;
using ma.metl.sirh.Service;
using System.Net;
using Resources;
using System.Web.Security;
using ma.metl.sirh.Common.TraceHistory;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Data.Odbc;


namespace ma.metl.sirh.Controllers
{
    public class UsersController : Controller
    {

        IUsersService usersService;
        IDirectionService directionService;
        IProfilService profilService;
        IServiceService serviceService;
        IAutorisationService autorisationservice;
        IAutorisation autorisation;
        IModuleService moduleservice;
        
             
        
        public UsersController()
        {

        }

        public UsersController(IUsersService UsersService, IDirectionService DirectionService, IProfilService ProfilService, IServiceService ServiceService, IAutorisationService AutorisationService, IAutorisation Autorisation, IModuleService ModuleService)
        {
            usersService = UsersService;
            directionService = DirectionService;
            profilService = ProfilService;
            serviceService = ServiceService;
            autorisationservice = AutorisationService;
            autorisation = Autorisation;
            moduleservice = ModuleService;
            
        }

        //
        // GET: /Users/

        public ActionResult Administration()
        {
            if (Session["Users"] == null)
            {
                return RedirectToAction("Login", "Users");

            }



            return View(profilService.GetAll());
        }

        public ActionResult AdministrationUsers()
        {
            if (Session["Users"] == null)
            {
                return RedirectToAction("Login", "Users");

            }
            
            ViewBag.Directions = new SelectList(directionService.GetAll(), "Id", "Description");
            ViewBag.Service = new SelectList(serviceService.GetAll(), "Id", "Description");
            ViewBag.Profils = new SelectList(profilService.GetAll(), "Id", "Libelle");

            return View(usersService.GetAll().Where(x => x.Statut == "1"));
        }
        [HttpPost]
        public ActionResult AdministrationUsers(string login, string identifiant, string NomPrenom, int? Profils, int? Directions)
        {
            ViewBag.Service = new SelectList(serviceService.GetAll(), "Id", "Description");
            ViewBag.Directions = new SelectList(directionService.GetAll(), "Id", "Description");
            ViewBag.Profils = new SelectList(profilService.GetAll(), "Id", "Libelle");
            return View(usersService.GetAll().Where(x => (x.Profil.Id == Profils || Profils == null) && (x.Direction.Id == Directions || Directions == null) && (x.Login == login || login == "") && (x.Identifiant == identifiant || identifiant == "") && (x.Name == NomPrenom || x.Prenom == NomPrenom || NomPrenom == "") && x.Statut == "1").ToList());
        }


        public ActionResult Index()
        {

            if (Session["Users"] == null)
            {
                return RedirectToAction("Login", "Users");

            }


            return View(usersService.GetAll());
        }

        public ActionResult Login()
        {

            return View();
        }
        public ActionResult MonCompte(string Login)
        {
            if (Session["Users"] == null)
            {
                return RedirectToAction("Login", "Users");

            }
            if (Login == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = usersService.GetByLogin(Login);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProfilId = new SelectList(profilService.GetAll().Where(x => x.Id == user.Profil.Id), "Id", "Libelle");
            ViewBag.DirectionId = new SelectList(directionService.GetAll().Where(x => x.Id == user.Direction.Id), "Id", "Description");
            ViewBag.ServiceId = new SelectList(serviceService.GetAll().Where(x => x.Id == user.Service.Id), "Id", "Description");
            if (user.Statut == "1") ViewBag.Stat = "Actif"; else ViewBag.Stat = "Inactif";
            return View(user);
        }
        [HttpPost]
        public ActionResult MonCompte(Users user, string oldpass, string newpass, string newpass2, FormCollection form)
        {
            string OldPass = FormsAuthentication.HashPasswordForStoringInConfigFile(oldpass, "MD5");

            string NewPass = FormsAuthentication.HashPasswordForStoringInConfigFile(newpass, "MD5");

            if (user.Password != OldPass)
            {
                ModelState.AddModelError("Oldpass", Profil_Localization.MSG01);
            }
            if (user.Password.Equals(OldPass) && newpass.Equals(newpass2))
            {
                string StatutAlt;
                user.Password = NewPass;
                user.ConfirmationPassword = NewPass;
                if (form["Stat"] == "Actif") StatutAlt = "1"; else StatutAlt = "2";
                user.Statut = StatutAlt;


                usersService.Update(user);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ProfilId = new SelectList(profilService.GetAll(), "Id", "Libelle", user.Profil);
            ViewBag.DirectionId = new SelectList(directionService.GetAll(), "Id", "Description", user.Direction);
            ViewBag.ServiceId = new SelectList(serviceService.GetAll(), "Id", "Description", user.Service);
            return RedirectToAction("Index", "Home");
        }

            ///*
            // * Connects to the specified ODBC source and returns the connection reference
            // */
            //protected OdbcConnection GetConnection(string connectString)
            //{
            //    OdbcConnection connection = null;

            //    // Open a connection
            //    try
            //      {            
            //        connection = new OdbcConnection(connectString);
            //        connection.Open();
            //      }
            //    catch (OdbcException ex)
            //      {
            //        var exp = ex.Message;
            //        return null;
            //      }
            //    return connection;
            //} 


            //protected CandidatGODto GetCandidatFromGipeOrd(string selectStatement, OdbcConnection connection)
            //{

            //        CandidatGODto candidat = new CandidatGODto(); ;          
           
            //        // Create a dataset
            //        System.Data.DataSet dataSet = new System.Data.DataSet();
            //        OdbcDataAdapter dataAdapter = new OdbcDataAdapter();
            //        OdbcCommandBuilder commandBuilder = new OdbcCommandBuilder(dataAdapter);
            //        dataAdapter.SelectCommand = new OdbcCommand(selectStatement, connection);
            //        dataAdapter.Fill(dataSet);
                            
            //        // Iterate through all the rows   
            //        var GipeOrd = dataSet.Tables;
            //        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            //        {
            //            foreach (System.Data.DataColumn column in dataSet.Tables[0].Columns)
            //            {
            //                Console.WriteLine(dataSet.Tables[0].Rows[i][column.ColumnName]);
            //                string columnName = column.ColumnName;
            //                switch (columnName)                         
            //                {
            //                    case "COD_AG":
            //                        candidat.NumDoti = dataSet.Tables[0].Rows[i][column.ColumnName].ToString();
            //                        break;
            //                    case "NOM_AG":
            //                        candidat.Nom = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break; 
            //                    case "PRENOM_AG":
            //                        candidat.Prenom = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "NOM_ARAB_AG":
            //                        candidat.NomAR = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "PRENOM_ARAB_AG":
            //                        candidat.PrenomAR = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "DAT_REC":
            //                        candidat.DateRecrutement = (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "ANN_N_AG":
            //                        candidat.AnnDateNaissance = short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());                      
            //                        break;
            //                    case "MOI_N_AG":
            //                        candidat.MoiDateNaissance = short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString()); 
            //                        break;
            //                    case "JOU_N_AG":
            //                        candidat.JourDateNaissance = short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString()); 
            //                        break;
            //                    case "SEX_AG":
            //                        candidat.Sexe = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "COD_LOC":                              
            //                        candidat.Localite = short.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString()); 
            //                        break; 
            //                    case "ANC_GRADE":
            //                        candidat.AncGrade = (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "ANC_ELO":
            //                        candidat.AncEchelon = (DateTime?)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "COD_S_AF_FTF":
            //                        candidat.Direction = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "CIN_A_AG":
            //                        candidat.CINA = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "CIN_N_AG":
            //                        candidat.CINN = int.Parse(dataSet.Tables[0].Rows[i][column.ColumnName].ToString());
            //                        break;
            //                    case "COD_GRADE":
            //                        candidat.CodeGrade = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "COD_CATEG":
            //                        candidat.CodeCateg = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "COD_CORPS":
            //                        candidat.CodeCorps = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                    case "COD_CADRE":
            //                        candidat.CodeCadre = (string)dataSet.Tables[0].Rows[i][column.ColumnName];
            //                        break;
            //                }
            //            }
            //        }

            //        if (String.IsNullOrEmpty(candidat.NumDoti))
            //        {
            //            candidat = null;
            //        }        
            //       return candidat;
            // } 
  
        [HttpPost]
        public ActionResult Login(string login, string password)
        {

            //// Specify Data Source Name DSN along with user and password
            //OdbcConnection connection = GetConnection("DSN=Connexion11g;UID=Gipeord;PWD=GipeOrd");

            //if (connection != null)
            //{
            //    try
            //    {
            //        string query = "SELECT  a.COD_AG , a.NOM_AG , a.PRENOM_AG, a.NOM_ARAB_AG , a.PRENOM_ARAB_AG, a.DAT_REC,"
            //                        +"a.ANN_N_AG, a.MOI_N_AG , a.JOU_N_AG, a.SEX_AG, a.COD_LOC, s.ANC_GRADE, s.ANC_ELO, " 
            //                        +"a.COD_S_AF_FTF, a.CIN_A_AG, a.CIN_N_AG, s.COD_GRADE, s.COD_CATEG, s.COD_CORPS," 
            //                        +"s.COD_CADRE FROM Agent a, SIT_ADM s WHERE a.COD_AG = s.COD_AG and a.COD_AG = 1986;";
            //        CandidatGODto dto = GetCandidatFromGipeOrd(query, connection);
            //    }

            //    finally
            //    {
            //        //Close DB connection and free resources
            //        connection.Dispose();
            //    }
            //}                 

            Users user = new Users();
            user = usersService.GetByLogin(login);
            string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

            if (login == "")
            {
                ModelState.AddModelError("login", "Le champ Login est obligatoire"); 
                return View();
            }

            if (user != null)
            {
                if (password == "")
                {
                    ModelState.AddModelError("Password", "Le champ Password est obligatoire");
                }
                if (user.Password != hashPassword && password != "")
                {
                    ModelState.AddModelError("LogPass", "Login ou mot de passe est incorrect");
                }
                if (user == null)
                {

                    return View(usersService.GetByLogin(login));
                }

                if (user.Statut == "2")
                {
                    ModelState.AddModelError("msgInfo", "Votre compte est inactif. Veuillez contacter votre administrateur!"); 
                    return View();
                } 

                if (user.Login != null && user.Password.Equals(hashPassword))
                {
                   
                    Session["Users"] = new Users()
                    {
                        Login = user.Login,
                        Id = user.Id,
                        Name = user.Name,
                        Profil = user.Profil,         
                    };

                    //Récupération de la liste des rôles utilisateurs 
                    List<string> listRoles = autorisationservice.GetRolesByProfil((int)user.Profil.Id);

                    Session["listRoles"] = listRoles;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            
            Session["Users"] = null;

            return RedirectToAction("Login", "Users");
        }

        public ActionResult Create()
        {
            if (Session["Users"] == null)
            {
                return RedirectToAction("Login", "Users");

            }

            ViewBag.ProfilId = new SelectList(profilService.GetAll(), "Id", "Libelle");
            ViewBag.DirectionId = new SelectList(directionService.GetAll(), "Id", "Description");
            ViewBag.ServiceId = new SelectList(serviceService.GetAll(), "Id", "Description");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users Users)
        {

            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(Users.Password, "MD5");
                Users.Password = hashPassword;
                Users.ConfirmationPassword = hashPassword;
                usersService.Create(Users);

                return RedirectToAction("AdministrationUsers", "Users");
            }

            ViewBag.ProfilId = new SelectList(profilService.GetAll(), "Id", "Libelle", Users.Profil);
            ViewBag.DirectionId = new SelectList(directionService.GetAll(), "Id", "Description", Users.Direction);
            ViewBag.ServiceId = new SelectList(serviceService.GetAll(), "Id", "Description", Users.Service);
            return View(Users);

        }

        public ActionResult CreateProfil()
        {
            return View(autorisation.GetAll());
        }

        [HttpPost]
        public ActionResult CreateProfil(FormCollection form)
        {
            string libelle = form["libelle"];
            if (libelle == "" || libelle == null)
            {
                ModelState.AddModelError("libelle", "Le champ libellé est obligatoire!");
            }
            else
            {
                Profil profil = new Profil();
                profil.Libelle = form["libelle"];
                profil.Code = form["libelle"];
                profilService.Create(profil);
                long Profil_Id = profilService.GetAll().Where(x => x.Libelle == form["libelle"]).First().Id;


                for (long i = 1; i <= autorisation.GetAll().Max(x => x.Id); i++)
                {
                    string Autorisation_Id = i.ToString();

                    if (form[Autorisation_Id] != null && form[Autorisation_Id].Contains("true"))
                    {

                        AutorisationProfil A = new AutorisationProfil();
                        A.Autorisation_Id = i;
                        A.Profil_Id = Profil_Id;
                        autorisationservice.Create(A);

                    }
                }
                ModelState.AddModelError("messageInfo", "Profil enregistré avec succès!");
            }
            return RedirectToAction("Administration", "Users");
        }

        public ActionResult ConsulterProfil(long? Id)
        {
            ViewBag.Profil = profilService.GetAll().Where(x => x.Id == Id).First().Libelle.ToString();

            ViewBag.AutorisationProfil = autorisationservice.GetAll().Where(x => x.Profil_Id == Id).ToList();
            return View(autorisation.GetAll());
        }

        public ActionResult ModifierProfil(long? Id)
        {
            ViewBag.Profil = profilService.GetAll().Where(x => x.Id == Id).First().Libelle.ToString();

            ViewBag.AutorisationProfil = autorisationservice.GetAll().Where(x => x.Profil_Id == Id).ToList();
            return View(autorisation.GetAll());
        }

        [HttpPost]
        public ActionResult ModifierProfil(FormCollection form)
        {
            string libelle = form["libelle"];
            if (libelle == "" || libelle == null)
            {
                ModelState.AddModelError("libelle", "Le champ libellé est obligatoire!");
            }
            else 
            {
                long Profil_Id = profilService.GetAll().Where(x => x.Libelle == form["libelle"]).First().Id;
                ViewBag.AutorisationProfil = autorisationservice.GetAll().Where(x => x.Profil_Id == Profil_Id).ToList();
                var AutorisationProfil = autorisationservice.GetAll().Where(x => x.Profil_Id == Profil_Id).ToList();
            foreach (AutorisationProfil AP in AutorisationProfil)
            {
                autorisationservice.Delete(AP);
            }


            for (long i = 1; i <= autorisation.GetAll().Max(x => x.Id); i++)
            {
                string Autorisation_Id = i.ToString();

                if (form[Autorisation_Id] != null && form[Autorisation_Id].Contains("true"))
                {

                    AutorisationProfil A = new AutorisationProfil();
                    A.Autorisation_Id = i;
                    A.Profil_Id = Profil_Id;
                    autorisationservice.Create(A);

                }
            }
            ModelState.AddModelError("messageInfo", "Modifications enregistrées avec succès!");
            ViewBag.Profil = profilService.GetAll().Where(x => x.Id == Profil_Id).First().Libelle.ToString();
            ViewBag.AutorisationProfil = autorisationservice.GetAll().Where(x => x.Profil_Id == Profil_Id).ToList();
            }
            return View(autorisation.GetAll());         
        }


        public ActionResult SupprimerProfil(long? Id)
        {
            long Profil_Id = profilService.GetAll().Where(x => x.Id == Id).First().Id;
            ViewBag.AutorisationProfil = autorisationservice.GetAll().Where(x => x.Profil_Id == Profil_Id).ToList();
            var AutorisationProfil = autorisationservice.GetAll().Where(x => x.Profil_Id == Profil_Id).ToList();
            foreach (AutorisationProfil AP in AutorisationProfil)
            {
                autorisationservice.Delete(AP);
            }
            Profil profil = profilService.GetAll().Where(x => x.Id == Id).First();
            profilService.Delete(profil);

            return RedirectToAction("Administration", "Users");

        }

        public ActionResult ModifierUtilisateur(string Login)
        {
            if (Session["Users"] == null)
            {
                return RedirectToAction("Login", "Users");

            }
            if (Login == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = usersService.GetByLogin(Login);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProfilId = new SelectList(profilService.GetAll(), "Id", "Libelle", user.Profil.Id);
            ViewBag.DirectionId = new SelectList(directionService.GetAll(), "Id", "Description", user.Direction.Id);
            ViewBag.ServiceId = new SelectList(serviceService.GetAll(), "Id", "Description", user.Service.Id);
            string statut = null;
            if (user.Statut == "1") statut = "Actif"; else statut = "Inactif";
            ViewBag.Statut = new SelectList(new[]
                                          {
                                              "Actif",
                                             "Inactif"
                                              
                                          }, statut);

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierUtilisateur(Users User)
        {
            if (ModelState.IsValid)
            {
                string StatutAlt = null;
                if (User.Statut == "Actif") StatutAlt = "1"; else StatutAlt = "2";



                User.Statut = StatutAlt;




                usersService.Update(User);
                return RedirectToAction("AdministrationUsers", new { Login = User.Login });
            }
            ViewBag.ProfilId = new SelectList(profilService.GetAll(), "Id", "Libelle");
            ViewBag.DirectionId = new SelectList(directionService.GetAll(), "Id", "Description");
            ViewBag.ServiceId = new SelectList(serviceService.GetAll(), "Id", "Description");
            string statut = null;
            if (User.Statut == "1") statut = "Actif"; else statut = "Inactif";
            ViewBag.Statut = new SelectList(new[]
                                          {
                                              "Actif",
                                             "Inactif"
                                              
                                          }, statut);
            return View();
        }
        public ActionResult ConsulterUtilisateur(string Login)
        {
            if (Session["Users"] == null)
            {
                return RedirectToAction("Login", "Users");

            }
            if (Login == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = usersService.GetByLogin(Login);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user.Statut == "1") ViewBag.Stat = "Actif"; else ViewBag.Stat = "Inactif";
            return View(user);
        }

        public ActionResult SupprimerUtilisateur(string Login)
        {
            if (Session["Users"] == null)
            {
                return RedirectToAction("Login", "Users");

            }
            if (Login == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = usersService.GetByLogin(Login);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.Statut = "2";
            usersService.Update(user);
            return RedirectToAction("AdministrationUsers", "Users");
        }
    }
}
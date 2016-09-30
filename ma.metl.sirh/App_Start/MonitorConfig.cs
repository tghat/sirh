using ma.metl.sirh.Controllers;
using ma.metl.sirh.Model;
using ma.metl.sirh.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Xml;


namespace ma.metl.sirh
{
    public class MonitorConfig
    {

        public static void RegisterWatchers()
        {
            string directoryInfo = ConfigurationManager.AppSettings["DirectoryInfo"];
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @directoryInfo+"\\GipeOrd";
            watcher.Filter = "*.xml";
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = false;
            watcher.Created += new FileSystemEventHandler(OnCreated);
        }

        static string GetEnumDescription<TEnum>(TEnum value)
        {
            string description; 
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            description = attributes[0].Description.ToString();
            return description;

        }

        static void OnCreated (object sender, FileSystemEventArgs e)
        {
            string directoryInfo = ConfigurationManager.AppSettings["DirectoryInfo"];
            Flux flux = new Flux();
            List<DetailAvancement> listDetailAvancement = new List<DetailAvancement>(); 
            sirhContext db = new sirhContext();
            string file = e.FullPath;
            string name = e.Name;

            // vérifier si le nom du fichier respecte le format communiqué par l'équipe métier 
            Regex regExp = new Regex("^(GipeOrd_AVG)(C|E)(_)((0?[1-9]|[12][0-9]|3[01])(0?[1-9]|1[012])\\d{4})(_)[0-9]{1,2}(\\.xml)$");

            // si le fichier existe déjà dans la base de données 

            Flux nameflux = db.Flux.FirstOrDefault(n => n.name == name);
            
            

            //Déplacer le fichier du répertoire GipeOrd à Outbox si le fichier est conforme ou le mettre dans le dossier Rejet si le fichier est non conforme 
            string sourceFilePath = @directoryInfo + "\\GipeOrd\\";
            string destFilePathOk = @directoryInfo + "\\Inbox\\";
            string destFilePathKo = @directoryInfo + "\\Rejet\\";

            string sourceFileName = System.IO.Path.Combine(sourceFilePath, name);
            string destFileNameOk = System.IO.Path.Combine(destFilePathOk, name);
            string destFileNameKo = System.IO.Path.Combine(destFilePathKo, name);

            if (regExp.IsMatch(name) && nameflux == null)
            {
                try {
                    
                    System.IO.File.Move(sourceFileName, destFileNameOk);
                    file = destFileNameOk;
                    
                     }catch(IOException exp){

                     }        
            }
            else
            {
                try
                {
                    System.IO.File.Move(sourceFileName, destFileNameKo);
                    file = destFileNameKo;
                    flux.TypeFlux = "";
                    flux.flux = file;
                    flux.name = name;
                    flux.anneeReception = DateTime.Now.Year.ToString();
                    flux.dateIntegration = DateTime.Now;
                    flux.Etat = GetEnumDescription(Etat.Rejete);
                    db.Flux.Add(flux);
                    db.SaveChanges();
                    return;
                }catch (IOException exp)
                { }
            }

           XmlDocument xmlDoc = new XmlDocument();
            // charge le fichier XML
           xmlDoc.Load(file);

            // retourne le nombre de lignes du fichier
            int nbrLigneFichier = xmlDoc.SelectNodes("PROMOGRADE/LIST_G_COD_AG/G_COD_AG").Count;

            //lecture du fichier 

            XmlNodeList promoGrade = xmlDoc.SelectNodes("/PROMOGRADE/LIST_G_COD_AG");
            XmlNodeList list = xmlDoc.GetElementsByTagName("G_COD_AG");
            List<LigneRejetee> lignesRejetees = new List<LigneRejetee>();
            List<Candidat> listeCandidat = new List<Candidat>();
            DetailAvancement detail;
            foreach(XmlNode node in list)
            {

                Candidat candidat = new Candidat();
                detail = new DetailAvancement(); 

                for(int i =0; i< node.ChildNodes.Count; i++)
                {
                    string nameChildNode = node.ChildNodes[i].Name;
                    string contenuChildNode = node.ChildNodes[i].InnerText;
                    if (nameChildNode == "ANC_GRADE" || nameChildNode == "DATE_EFF_MVT")
                    {
                        try
                        {
                            Convert.ToDateTime(contenuChildNode);
                        }
                        catch(FormatException excp)
                        {
                            Console.WriteLine(excp.GetType().Name);
                            LigneRejetee ligneRejetee = new LigneRejetee();
                            ligneRejetee.detail = node.ToString();
                            ligneRejetee.motifRejet = "Date incorrecte!";
                            lignesRejetees.Add(ligneRejetee); 
                            break;
                        }
                    }

                }
                
                candidat.NumDoti = node.ChildNodes[0].InnerText;

                // à revoir avec le client 

                //candidat.AncienGrade = node.ChildNodes[13].InnerText;
                //candidat.NouveauGrade = node.ChildNodes[14].InnerText;
                candidat.Nom = node.ChildNodes[15].InnerText;
                listeCandidat.Add(candidat);
                detail.DateAncienGrade = Convert.ToDateTime(node.ChildNodes[9].InnerText);
                detail.DateEff = Convert.ToDateTime(node.ChildNodes[10].InnerText);
                
                //detail.AncienGrade = node.ChildNodes[13].InnerText;
                //detail.NouveauGrade = node.ChildNodes[14].InnerText;
                
                listDetailAvancement.Add(detail); 
            }

            // informations concernant le fichier 
            flux.TypeFlux = "";
            flux.Source = "GipeOrd"; 
            flux.flux = file;
            flux.name = name; 
            flux.anneeReception = DateTime.Now.Year.ToString();
            flux.dateIntegration = DateTime.Now;
            flux.nbrTotalLigne = nbrLigneFichier;
            flux.nbrLigneRejete = lignesRejetees.Count; 

            if (lignesRejetees.Count == nbrLigneFichier)
            {
                flux.Etat = GetEnumDescription(Etat.Rejete);
            }
            else
            {
                flux.Etat = GetEnumDescription(Etat.EnCours);
            }
            //créer le fichier accusé de réception 
            
            string pathAccuse = @directoryInfo + "\\Outbox\\" + Path.GetFileNameWithoutExtension(name) + "_AR.txt";
            //TextWriter tw = new StreamWriter(pathAccuse);
            String txt = "Nombre total de lignes est de :" + nbrLigneFichier +"\n" + "Nombre de lignes rejetée est de :" + lignesRejetees.Count+"\n" + "nombre de lignes inserées est de :" + listeCandidat.Count;
            System.IO.File.WriteAllText(pathAccuse, txt); 
           
            // inserer le contenu dans la base de données

            foreach (Candidat c in listeCandidat)
            {
                db.Candidat.Add(c);
                
            }
            
            foreach (LigneRejetee ligne in lignesRejetees)
            {
                db.LigneRejetee.Add(ligne);
            }

            foreach (DetailAvancement detailAvc in listDetailAvancement)
            {
                db.DetailAvancement.Add(detailAvc);
            }

            db.Flux.Add(flux);
            db.SaveChanges();
            return; 
           
            
        }
    }

   
}
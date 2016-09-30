using System.Collections.Generic;
using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Ora;
using ma.metl.sirh.Model.Dto;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System;


namespace ma.metl.sirh.Repository
{
    public class CandidatRepository : GenericRepository<Candidat>, ICandidatRepository
    {
        private DbContext context; 
        public CandidatRepository(sirhContext context)
            : base(context)
        {
            this.context = context; 
        }

        public Candidat GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public Candidat GetByNumDoti(string numDoti)
        {         
            return FindBy(x => x.NumDoti.Equals(numDoti)).FirstOrDefault();
        }

        public bool isExistingCandidat(string numDoti, string annee)
        {

            var db = new sirhContext();
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();   
            bool flag = true;
            var query = (from c in db.Candidat
                         join d in db.DetailAvancement
                         on c.Id equals d.CandidatId
                         where annee.Equals(d.Annee) && c.NumDoti == numDoti
                         select new CandidatResultatDto
                         {
                             Id = c.Id,
                         });
           if (!query.ToList().Any())    
           { 
               flag = false;
           }          
           return flag;
        }

  
        //Retourne les candidats selon les filtres utilisateurs 
        public List<CandidatResultatDto> GetAllCandidatAuChoix(CandidatCritereRechDto dto, DateTime? AncienneteGrade, DateTime? AncienneteEchelon)
        {
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();           

            var db = new sirhContext();
            
            var query = (from c in db.Candidat
                         join d in db.DetailAvancement
                         on c.Id equals d.CandidatId
                         where d.Flux.TypeFlux == "AC" && d.Flux.Etat == "Valide"
                         select new CandidatResultatDto
                         {
                             Id = c.Id,
                             NumDoti = c.NumDoti,
                             Nom = c.Nom,
                             Prenom = c.Prenom,
                             AncienGrade = d.AncienGrade.Description,
                             NouveauGrade = d.NouveauGrade.Description,
                             DateEffet = d.DateEff,
                             Localite = c.Localite.Intitule,
                             Direction = c.Direction.Description,
                             Annee = d.Annee,
                             AncienneteGrade = d.AncienneteGrade,
                             AncienneteEchelon = d.AncienneteEchelon,
                             GradeId = (int)d.GradeIdAncien,
                             LocaliteId = (int)c.LocaliteId,
                             DateNaissance = c.DateNaissance,
                             DateRecrutement = c.DateRecrutement,
                             Statut =  d.Statut,
                             DecisionCap = d.DecisionCap,
                             NoteMoyenne = d.Note,
                             detailAvancement = d.Id
                         });

            candidats = query.ToList();
            
            if (!String.IsNullOrEmpty(dto.AnneeProm))
                candidats = candidats.Where(x => x.Annee == dto.AnneeProm).ToList();

            if (!String.IsNullOrEmpty(dto.NumDoti))
                candidats = candidats.Where(x => x.NumDoti == dto.NumDoti).ToList();

            if (dto.GradeId != 0)
                candidats = candidats.Where(x => x.GradeId == dto.GradeId).ToList();

            if (dto.RegionId != 0)
                candidats = candidats.Where(x => x.LocaliteId == dto.RegionId).ToList();

            if (AncienneteGrade != null)
                candidats = candidats.Where(x => x.AncienneteGrade == AncienneteGrade).ToList();

            if (AncienneteEchelon != null)
                candidats = candidats.Where(x => x.AncienneteEchelon == AncienneteEchelon).ToList();

            if (dto.DateNaissance != null)
                candidats = candidats.Where(x => x.DateNaissance == dto.DateNaissance).ToList();

            if (dto.DateRecrutement != null)
                candidats = candidats.Where(x => x.DateRecrutement == dto.DateRecrutement).ToList();

            return candidats;
        }

        //Retourne les candidats selon les filtres utilisateurs 
        public List<CandidatResultatDto> GetCandidatAuChoixById(int id)
        {
            List<CandidatResultatDto> candidat = new List<CandidatResultatDto>();

            var db = new sirhContext();

            var query = (from c in db.Candidat
                         join d in db.DetailAvancement
                         on c.Id equals d.CandidatId
                         where d.Flux.TypeFlux == "AC" && d.Flux.Etat == "Valide" && (int)c.Id == id
                         select new CandidatResultatDto
                         {
                             Id = c.Id,
                             NumDoti = c.NumDoti,
                             Nom = c.Nom,
                             NomA = c.NomAR,
                             Prenom = c.Prenom,
                             PrenomA = c.PrenomAR,
                             AncienGrade = d.AncienGrade.Description,
                             AncienGradeA = d.AncienGrade.DescriptionAM,
                             AncienGradeCadreCode = d.AncienGrade.CodeCadre,
                             AncienGradeCode = d.AncienGrade.CodeGrade,
                             NouveauGrade = d.NouveauGrade.Description,
                             NouveauGradeA = d.NouveauGrade.DescriptionAM,
                             NouveauGradeCadreCode = d.NouveauGrade.CodeCadre,
                             NouveauGradeCode = d.NouveauGrade.CodeGrade,
                             DateEffet = d.DateEff,
                             Localite = c.Localite.Intitule,
                             Direction = c.Direction.Description,
                             DirectionA = c.Direction.DescriptionA,
                             Annee = d.Annee,
                             AncienneteGrade = d.AncienneteGrade,
                             AncienneteEchelon = d.AncienneteEchelon,
                             GradeId = (int)d.GradeIdAncien,
                             LocaliteId = (int)c.LocaliteId,
                             DateNaissance = c.DateNaissance,
                             DateRecrutement = c.DateRecrutement,
                             Statut = d.Statut,
                             DecisionCap = d.DecisionCap,
                             NoteMoyenne = d.Note,
                             detailAvancement = d.Id
                         });

            candidat = query.ToList();

            

            return candidat;
        }

        //Retourne les candidats selon les filtres utilisateurs 
        public List<CandidatResultatDto> GetCandidatExamenById(int id)
        {
            List<CandidatResultatDto> candidat = new List<CandidatResultatDto>();

            var db = new sirhContext();

            var query = (from c in db.Candidat
                         join d in db.DetailAvancement
                         on c.Id equals d.CandidatId
                         where d.Flux.TypeFlux == "AE" && d.Flux.Etat == "Valide" && (int)c.Id == id
                         select new CandidatResultatDto
                         {
                             Id = c.Id,
                             NumDoti = c.NumDoti,
                             Nom = c.Nom,
                             Prenom = c.Prenom,
                             AncienGrade = d.AncienGrade.Description,
                             NouveauGrade = d.NouveauGrade.Description,
                             DateEffet = d.DateEff,
                             Localite = c.Localite.Intitule,
                             Direction = c.Direction.Description,
                             Annee = d.Annee,
                             AncienneteGrade = d.AncienneteGrade,
                             AncienneteEchelon = d.AncienneteEchelon,
                             GradeId = (int)d.GradeIdAncien,
                             LocaliteId = (int)c.LocaliteId,
                             DateNaissance = c.DateNaissance,
                             DateRecrutement = c.DateRecrutement,
                             Statut = d.Statut,
                             DecisionCap = d.DecisionCap,
                             NoteMoyenne = d.Note,
                             detailAvancement = d.Id
                         });

            candidat = query.ToList();



            return candidat;
        }
        //Retourne les candidats selon les filtres utilisateurs 
        public List<CandidatResultatDto> GetAllCandidatAuChoix()
        {
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();

            var db = new sirhContext();

            var query = (from c in db.Candidat
                         join d in db.DetailAvancement
                         on c.Id equals d.CandidatId
                         where d.Flux.TypeFlux == "AC" && d.Flux.Etat == "Valide"
                         select new CandidatResultatDto
                         {
                             Id = c.Id,
                             NumDoti = c.NumDoti,
                             Nom = c.Nom,
                             Prenom = c.Prenom,
                             AncienGrade = d.AncienGrade.Description,
                             NouveauGrade = d.NouveauGrade.Description,
                             DateEffet = d.DateEff,
                             Localite = c.Localite.Intitule,
                             Direction = c.Direction.Description,
                             Annee = d.Annee,
                             AncienneteGrade = d.AncienneteGrade,
                             AncienneteEchelon = d.AncienneteEchelon,
                             GradeId = (int)d.GradeIdAncien,
                             LocaliteId = (int)c.LocaliteId,
                             DateNaissance = c.DateNaissance,
                             DateRecrutement = c.DateRecrutement,
                             Statut = d.Statut,
                             DecisionCap = d.DecisionCap,
                             NoteMoyenne = d.Note,
                             detailAvancement = d.Id
                         });

            return candidats = query.ToList();     
        }

        //Retourne les candidats selon les filtres utilisateurs 
        public List<CandidatResultatDto> GetAllCandidatSurExamen()
        {
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();

            var db = new sirhContext();

            var query = (from c in db.Candidat
                         join d in db.DetailAvancement
                         on c.Id equals d.CandidatId
                         where d.Flux.TypeFlux == "AE" && d.Flux.Etat == "Valide"
                         select new CandidatResultatDto
                         {
                             Id = c.Id,
                             NumDoti = c.NumDoti,
                             Nom = c.Nom,
                             Prenom = c.Prenom,
                             AncienGrade = d.AncienGrade.Description,
                             NouveauGrade = d.NouveauGrade.Description,
                             DateEffet = d.DateEff,
                             Localite = c.Localite.Intitule,
                             Direction = c.Direction.Description,
                             Annee = d.Annee,
                             AncienneteGrade = d.AncienneteGrade,
                             AncienneteEchelon = d.AncienneteEchelon,
                             GradeId = (int)d.GradeIdAncien,
                             LocaliteId = (int)c.LocaliteId,
                             DateNaissance = c.DateNaissance,
                             DateRecrutement = c.DateRecrutement,
                             Statut = d.Statut,
                             DecisionCap = d.DecisionCap,
                             NoteMoyenne = d.Note,
                             detailAvancement = d.Id
                         });

            return candidats = query.ToList();
        }

        public List<CandidatResultatDto> GetAllCandidatures(CandidatCritereRechDto dto)
        {
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();
            var db = new sirhContext();
            var query = (from c in db.Candidat
                         join d in db.Candidature
                         on c.Id equals d.CandidatId
                         select new CandidatResultatDto
                         {
                             Id = c.Id,
                             NumDoti = c.NumDoti,
                             Nom = c.Nom,
                             Prenom = c.Prenom,
                             Localite = c.Localite.Intitule,
                             Direction = c.Direction.Description,
                             Statut = d.Etat,
                             AncienGrade = c.Grade.Description,
                             DateEffet = c.DateEffet,
                             candidatureId = d.Id,
                             Annee = d.Annee,
                             GradeId = (int) c.GradeId,
                             DateRecrutement = c.DateRecrutement,
                         });

           candidats = query.ToList();

           if (!String.IsNullOrEmpty(dto.AnneeProm))
               candidats = candidats.Where(x => x.Annee == dto.AnneeProm).ToList();

           if (dto.GradeId != 0)
               candidats = candidats.Where(x => x.GradeId == dto.GradeId).ToList();

           if (dto.SpecialiteId != 0)
               candidats = candidats.Where(x => x.SpecialiteId == dto.SpecialiteId).ToList();

           //if (AncienneteGrade != null)
           //    candidats = candidats.Where(x => x.AncienneteGrade == AncienneteGrade).ToList();

          
           if (dto.DateRecrutement != null)
               candidats = candidats.Where(x => x.DateRecrutement == dto.DateRecrutement).ToList();

           if (!("Selectionnez").Equals(dto.EtatCandidature.ToString()))
               candidats = candidats.Where(x => x.Statut == dto.EtatCandidature.ToString()).ToList();

           return candidats;
        }

        //retourne la liste des candidats pour traitement au niveau du module gestion des examens 

        public List<CandidatResultatDto> GetAllCandidatSurExamen(string anneProm, int Ngrade)
        {
            int NouveauGrade = Convert.ToInt32(Ngrade);

            var db = new sirhContext();
            var query = from c in db.Candidat
                        join d in db.DetailAvancement
                        on c.Id equals d.CandidatId
                        where d.Flux.TypeFlux == "AE" && d.Flux.Etat == "Valide" && d.Annee == anneProm 
                        && d.NouveauGrade.Id == NouveauGrade
                        select new CandidatResultatDto
                        {
                            NumDoti = c.NumDoti,
                            Nom = c.Nom,
                            Prenom = c.Prenom, 
                            AncienGrade = d.AncienGrade.Description,
                            NouveauGrade = d.NouveauGrade.Description,
                            DateEffet = d.DateEff,
                            Localite = c.Localite.Intitule,
                            Annee = d.Annee,
                            AncienneteGrade = d.AncienneteGrade
                        };
        
            return query.ToList();
        }

        //Retourne les candidats selon les filtres utilisateurs 
        public List<CandidatResultatDto> GetAllCandidatExamen(CandidatCritereRechDto dto, DateTime? AncienneteGrade, DateTime? AncienneteEchelon)
        {
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();
            CommissionRepository comm = new CommissionRepository((sirhContext)context); 
            Commission commission = comm.GetById(dto.CommissionId);
            var db = new sirhContext();

            var query = (from c in db.Candidat
                         join d in db.DetailAvancement
                         on c.Id equals d.CandidatId
                         where d.Flux.TypeFlux == "AE" && d.Flux.Etat == "Valide"
                         select new CandidatResultatDto
                         {
                             Id = c.Id,
                             NumDoti = c.NumDoti,
                             Nom = c.Nom,
                             Prenom = c.Prenom,
                             AncienGrade = d.AncienGrade.Description,
                             NouveauGrade = d.NouveauGrade.Description,
                             DateEffet = d.DateEff,
                             Localite = c.Localite.Intitule,
                             Direction = c.Direction.Description,
                             Annee = d.Annee,
                             AncienneteGrade = d.AncienneteGrade,
                             AncienneteEchelon = d.AncienneteEchelon,
                             GradeId = (int)d.GradeIdAncien,
                             DirectionId = (int)c.DirectionId,
                             LocaliteId = (int)c.LocaliteId,
                             DateNaissance = c.DateNaissance,
                             DateRecrutement = c.DateRecrutement,
                             Statut = d.Statut,
                             DecisionCap = d.DecisionCap,
                             NoteMoyenne = d.Note,
                             detailAvancement = d.Id
                         });

            candidats = query.ToList();

            if (!String.IsNullOrEmpty(dto.AnneeProm))
                candidats = candidats.Where(x => x.Annee == dto.AnneeProm).ToList();

            if (!String.IsNullOrEmpty(dto.NumDoti))
                candidats = candidats.Where(x => x.NumDoti == dto.NumDoti).ToList();

            if (dto.GradeId != 0)
                candidats = candidats.Where(x => x.GradeId == dto.GradeId).ToList();

            if (dto.DirectionId != 0)
                candidats = candidats.Where(x => x.DirectionId == dto.DirectionId).ToList();

            if (dto.RegionId != 0)
                candidats = candidats.Where(x => x.LocaliteId == dto.RegionId).ToList();

            if (AncienneteGrade != null)
                candidats = candidats.Where(x => x.AncienneteGrade == AncienneteGrade).ToList();

            if (AncienneteEchelon != null)
                candidats = candidats.Where(x => x.AncienneteEchelon == AncienneteEchelon).ToList();

            if (dto.DateNaissance != null)
                candidats = candidats.Where(x => x.DateNaissance == dto.DateNaissance).ToList();

            if (dto.DateRecrutement != null)
                candidats = candidats.Where(x => x.DateRecrutement == dto.DateRecrutement).ToList();
            if (dto.CommissionId != 0)
                candidats = candidats.Where(x => x.GradeId == dto.GradeId && x.Annee==commission.Annee).ToList();
           

            return candidats;
        }

        //public List<InterfacageActeCandidatResultatVM> GetCandidatsByCriteria(string annee, long? gradeId, string statut)
        //{
        //    var db = new sirhContext();
        //    var candidtats = from c in db.Candidat
        //                     join da in db.DetailAvancement on c.Id equals da.CandidatId
        //                     //where da.Statut.ToLower().Equals("retenu") 
        //                     //   && da.Annee.ToLower().Equals(annee) 
        //                     //   && da.GradeIdNouveau == gradeId
        //                     select new InterfacageActeCandidatResultatVM()
        //                     {
        //                         CandidatId = c.Id,
        //                         DetailAvancementId = da.Id,
        //                         NumDoti = c.NumDoti,
        //                         CandidatNom = c.Nom,
        //                         CandidatPrenom = c.Prenom,
        //                         DeatilAvancementStatut = da.Statut
        //                     };

        //    return candidtats.ToList<InterfacageActeCandidatResultatVM>();
        //}
    }
}

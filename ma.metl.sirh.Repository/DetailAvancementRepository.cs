using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class DetailAvancementRepository : GenericRepository<DetailAvancement>, IDetailAvancementRepository
    {
        public DetailAvancementRepository(sirhContext context)
            : base(context)
        {

        }

        public List<DetailAvancement> GetDetailByNumDoti(string numDoti)
        {
            return FindBy(x => x.Candidat.NumDoti.Equals(numDoti)).ToList();
        }

        public int GetNombreTotalPromouvableAC(int grade, string annee)
        {
            return FindBy(x => x.Annee.Equals(annee) && x.NouveauGrade.Id == grade && x.Statut.Equals(EtatDetailAVC.Valide.ToString()) && x.Flux.TypeFlux.Equals("AC")).Count(); 
             
        }

        public int GetNombreTotalPromouvableAE(int grade, string annee)
        {
            var list = GetById(1);
            return FindBy(x => x.Annee.Equals(annee) && x.NouveauGrade.Id == grade && x.Statut.Equals("Valide") && x.Flux.TypeFlux.Equals("AE")).Count();

        }

        public DetailAvancement GetById(int Id) {
            return FindBy(x => x.Id == Id).FirstOrDefault(); 
        }

        public List<DetailAvancement> GetByGradeAnneeAC(int grade, string annee)
        {
            return FindBy(x => x.Annee.Equals(annee) && x.NouveauGrade.Id == grade && x.DecisionCap.Equals("Retenu") && x.Flux.TypeFlux.Equals("AC")).OrderBy(x => x.Note).ToList();

        }

        public List<CandidatResultatDto> GetByGradeAnneeAEEcrit(int grade, string annee)
        {
            var db = new sirhContext();
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();

            var query = (from d in db.DetailAvancement
                         join n in db.Notation
                         on d.Id equals n.DetailAvancement.Id
                         where d.Annee.Equals(d.Annee) && d.GradeIdNouveau == grade && d.Flux.TypeFlux == "AE"
                         select new CandidatResultatDto
                         {
                             Nom = d.Candidat.Nom,
                             Prenom = d.Candidat.Prenom,
                             NoteEcrite = n.NoteEcrite
                         });
            return query.ToList(); 

        }

        public List<CandidatResultatDto> GetByGradeAnneeAEOral(int grade, string annee)
        {
            var db = new sirhContext();
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();

            var query = (from d in db.DetailAvancement
                         join n in db.Notation
                         on d.Id equals n.DetailAvancement.Id
                         where d.Annee.Equals(d.Annee) && d.GradeIdNouveau == grade && d.Flux.TypeFlux == "AE"
                         select new CandidatResultatDto
                         {
                             Nom = d.Candidat.Nom,
                             Prenom = d.Candidat.Prenom,
                             NoteEcrite = n.NoteGlobale
                         });
            return query.ToList();

        }

        public List<CandidatResultatDto> GetByGradeAnneeAEGlobal(int grade, string annee)
        {
            var db = new sirhContext();
            List<CandidatResultatDto> candidats = new List<CandidatResultatDto>();

            var query = (from d in db.DetailAvancement
                         join n in db.Notation
                         on d.Id equals n.DetailAvancement.Id
                         where d.Annee.Equals(d.Annee) && d.GradeIdNouveau == grade && d.Flux.TypeFlux == "AE"
                         select new CandidatResultatDto
                         {
                             Nom = d.Candidat.Nom,
                             Prenom = d.Candidat.Prenom,
                             NoteEcrite = n.NoteGlobale
                         });
            return query.ToList();

        }
    }
}

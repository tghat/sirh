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
    public class ParametrageQuotaRepository : GenericRepository<ParametrageQuota>, IParametrageQuotaRepository
    {
        public ParametrageQuotaRepository(sirhContext context)
            : base(context)
        {
           
        }
        public ParametrageQuota GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();            
        }

        public ParametrageQuota GetNbrPoste(string annee, string grade)
        {
            int id = Convert.ToInt32(grade); 
            return FindBy(x=>x.GradeAcces.Id == id && x.Annee.Equals(annee)).FirstOrDefault(); 
        }

        public List<QuotaDto> GetTableauQuotaAC(string annee, int grade)
        {
            int id = Convert.ToInt32(grade);
            var db = new sirhContext();

            var query = (from c in db.ParametrageQuota
                         join d in db.Grade
                         on c.GradeIdAcces equals d.Id
                         where c.Annee.Equals(annee) && c.TypeFlux == "AC" && c.GradeIdAcces == grade
                         select new QuotaDto
                         {
                             id = c.Id,
                             Grade = d.Description,
                             GradeA = d.DescriptionAM,
                             Quota = c.Quota,
                             Commentaire = c.Commentaire,
                             StatutTQ = c.Statut,
                             Annee = c.Annee
                         });

            return query.ToList();
        }

        public List<QuotaDto> GetTableauQuotaAE(string annee, int grade)
        {
            int id = Convert.ToInt32(grade);
            var db = new sirhContext();

            var query = (from c in db.ParametrageQuota
                         join d in db.Grade
                         on c.GradeIdAcces equals d.Id
                         where c.Annee.Equals(annee) && c.TypeFlux == "AE" && c.GradeIdAcces == grade
                         select new QuotaDto
                         {
                             id = c.Id,
                             Grade = d.Description,
                             Quota = c.Quota,
                             Commentaire = c.Commentaire,
                             StatutTQ = c.Statut
                         });

            return query.ToList();
        }

        public QuotaDto GetNbrPosteAC(string annee, int grade)
        {
            int id = Convert.ToInt32(grade);
            var db = new sirhContext();

            var query = (from c in db.ParametrageQuota
                         join d in db.Grade
                         on c.GradeIdAcces equals d.Id
                         where c.Annee.Equals(annee) && c.TypeFlux == "AC" && c.GradeIdAcces == grade && c.Statut=="Vise"
                         select new QuotaDto
                         {
                             id = c.Id,
                             Grade = d.Description,
                             Quota = c.Quota,
                             Commentaire = c.Commentaire,
                             StatutTQ = c.Statut,
                             NbrPosteOuvrir= c.NbrPoste
                         });

            return query.ToList().FirstOrDefault();
        }

        public QuotaDto GetNbrPosteAE(string annee, int grade)
        {
            int id = Convert.ToInt32(grade);
            var db = new sirhContext();

            var query = (from c in db.ParametrageQuota
                         join d in db.Grade
                         on c.GradeIdAcces equals d.Id
                         where c.Annee.Equals(annee) && c.TypeFlux == "AE" && c.GradeIdAcces == grade && c.Statut == "Vise"
                         select new QuotaDto
                         {
                             id = c.Id,
                             Grade = d.Description,
                             Quota = c.Quota,
                             Commentaire = c.Commentaire,
                             StatutTQ = c.Statut,
                             NbrPosteOuvrir = c.NbrPoste
                         });

            return query.ToList().FirstOrDefault();
        }

    }
}

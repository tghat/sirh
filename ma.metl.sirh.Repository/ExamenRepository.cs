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
    public class ExamenRepository : GenericRepository<Examen>, IExamenRepository
    {
        public ExamenRepository(sirhContext context)
            : base(context)
        {
           
        }

        public Examen GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();            
        }

        public Examen GetByAnneeAndIdGrade(string Annee, int idGrade)
        {
            return FindBy(x => x.Grade.Id == idGrade && x.Annee.Equals(Annee)).FirstOrDefault();
        }

        public List<ResultatRapportDto> GetListExamen()
        {
            string annee = DateTime.Now.Year.ToString();  
            var db = new sirhContext();
            var query = (from c in db.Examen
                         join d in db.Grade
                         on c.GradeId equals d.Id
                         where c.Annee == annee
                         select new ResultatRapportDto
                         {
                             IntituleGrade = d.DescriptionAM,
                             nbrTotalannee = c.nbrCandidatEligibleAnnee,
                             nbrTotalDate = c.nbrCandidatEligibleDateExam,
                             nbrRestant = c.nbrCandidatEligibleAnnee - c.nbrCandidatEligibleDateExam,
                             pourcentageRestant = (c.nbrCandidatEligibleAnnee - c.nbrCandidatEligibleDateExam) * 100 / c.nbrCandidatEligibleAnnee,
                             pourcentageDateExamen =  c.nbrCandidatEligibleDateExam * 100 /c.nbrCandidatEligibleAnnee,
                             AnneeExam = c.Annee,
                             DateExamen = c.DateExamen
                         });
            return query.ToList();
        }

    }
}

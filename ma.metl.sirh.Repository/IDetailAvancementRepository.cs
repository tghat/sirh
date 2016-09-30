using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface IDetailAvancementRepository : IGenericRepository<DetailAvancement>
    {
        
        List<DetailAvancement> GetDetailByNumDoti(string numDoti);
        DetailAvancement GetById(int Id);
        int GetNombreTotalPromouvableAC(int grade, string annee);
        int GetNombreTotalPromouvableAE(int grade, string annee);
        List<DetailAvancement> GetByGradeAnneeAC(int grade, string annee);
        List<CandidatResultatDto> GetByGradeAnneeAEEcrit(int grade, string annee);
        List<CandidatResultatDto> GetByGradeAnneeAEOral(int grade, string annee);
        List<CandidatResultatDto> GetByGradeAnneeAEGlobal(int grade, string annee);
    }
}

using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface IParametrageQuotaRepository : IGenericRepository<ParametrageQuota>
    {
        ParametrageQuota GetById(int id);
        ParametrageQuota GetNbrPoste(string annee, string grade);
        List<QuotaDto> GetTableauQuotaAC(string annee, int grade);
        List<QuotaDto> GetTableauQuotaAE(string annee, int grade);
        QuotaDto GetNbrPosteAC(string annee, int grade);
        QuotaDto GetNbrPosteAE(string annee, int grade);
    }
}

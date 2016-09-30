using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class ParametrageQuotaService : EntityService<ParametrageQuota>, IParametrageQuotaService
    {
        IUnitOfWork _unitOfWork;
        IParametrageQuotaRepository _ParametrageQuotaRepository;

        public ParametrageQuotaService(IUnitOfWork unitOfWork, IParametrageQuotaRepository ParametrageQuotaRepository)
            : base(unitOfWork, ParametrageQuotaRepository)
        {
            _unitOfWork = unitOfWork;
            _ParametrageQuotaRepository = ParametrageQuotaRepository;
        }


        public ParametrageQuota GetById(int Id)
        {
            return _ParametrageQuotaRepository.GetById(Id);
        }

        public ParametrageQuota GetNbrPoste(string annee, string grade)
        {
            return _ParametrageQuotaRepository.GetNbrPoste(annee, grade); 
        }

        public List<QuotaDto> GetTableauQuotaAC(string annee, int grade)
        {
            return _ParametrageQuotaRepository.GetTableauQuotaAC(annee, grade);
        }

        public List<QuotaDto> GetTableauQuotaAE(string annee, int grade)
        {
            return _ParametrageQuotaRepository.GetTableauQuotaAE(annee, grade);
        }

        public QuotaDto GetNbrPosteAC(string annee, int grade)
        {
            return _ParametrageQuotaRepository.GetNbrPosteAC(annee, grade);
        }

        public QuotaDto GetNbrPosteAE(string annee, int grade)
        {
            return _ParametrageQuotaRepository.GetNbrPosteAC(annee, grade);
        }
    }
}

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
    public class ReunionService  : EntityService<Reunion>, IReunionService
    {
        IUnitOfWork _unitOfWork;
        IReunionRepository _ReunionRepository;

        public ReunionService(IUnitOfWork unitOfWork, IReunionRepository ReunionRepository)
            : base(unitOfWork, ReunionRepository)
        {
            _unitOfWork = unitOfWork;
            _ReunionRepository = ReunionRepository;
        }


        public Reunion GetById(int Id)
        {
            return _ReunionRepository.GetById(Id);
        }

        public List<EventDto> GetListReunion(DateTime fromDate, DateTime toDate)
        {
            return _ReunionRepository.GetListReunion(fromDate, toDate);
        }

        public List<EventDto> GetListReunion(DateTime fromDate, DateTime toDate, int commissionId, int gradeId, string annee, string examenType)
        {
            return _ReunionRepository.GetListReunion(fromDate, toDate, commissionId, gradeId, annee, examenType);
        }

        public List<Reunion> GetReunionByDate(int idCom)
        {
            return _ReunionRepository.GetReunionByDate(idCom); 
        }
    }
}

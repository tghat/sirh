using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class MatiereExamenService : EntityService<MatiereExamen>, IMatiereExamenService
    {
        IUnitOfWork _unitOfWork;
        IMatiereExamenRepository _MatiereExamenRepository;

        public MatiereExamenService(IUnitOfWork unitOfWork, IMatiereExamenRepository MatiereExamenRepository)
            : base(unitOfWork, MatiereExamenRepository)
        {
            _unitOfWork = unitOfWork;
            _MatiereExamenRepository = MatiereExamenRepository;
        }


        public MatiereExamen GetById(int Id)
        {
            return _MatiereExamenRepository.GetById(Id);
        }

        public MatiereExamen GetByIdMatiereIdExamen(long idMatiere, long idExamen)
        {
            return _MatiereExamenRepository.GetByIdMatiereIdExamen(idMatiere, idExamen);
        }
       
    }
}

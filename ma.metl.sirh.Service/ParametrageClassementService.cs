using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class ParametrageClassementService : EntityService<ParametrageClassement>, IParametrageClassementService 
    {
         IUnitOfWork _unitOfWork;
        IParametrageClassementRepository _ParametrageClassementRepository;

        public ParametrageClassementService(IUnitOfWork unitOfWork, IParametrageClassementRepository ParametrageClassementRepository)
            : base(unitOfWork, ParametrageClassementRepository)
        {
            _unitOfWork = unitOfWork;
            _ParametrageClassementRepository = ParametrageClassementRepository;
        }

        public ParametrageClassement GetById(int id)
        {
            return _ParametrageClassementRepository.GetById(id);
        }
    }
}

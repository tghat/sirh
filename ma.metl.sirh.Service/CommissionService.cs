using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class CommissionService : EntityService<Commission>, ICommissionService
    {
        IUnitOfWork _unitOfWork;
        ICommissionRepository _CommissionRepository;

        public CommissionService(IUnitOfWork unitOfWork, ICommissionRepository CommissionRepository)
            : base(unitOfWork, CommissionRepository)
        {
            _unitOfWork = unitOfWork;
            _CommissionRepository = CommissionRepository;
        }


        public Commission GetById(int Id)
        {
            return _CommissionRepository.GetById(Id);
        }
    }
}

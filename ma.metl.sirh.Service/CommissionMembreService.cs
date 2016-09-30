using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class CommissionMembreService : EntityService<CommissionMembre>, ICommissionMembreService
    {
        IUnitOfWork _unitOfWork;
        ICommissionMembreRepository _CommissionMembreRepository;

        public CommissionMembreService(IUnitOfWork unitOfWork, ICommissionMembreRepository CommissionMembreRepository)
            : base(unitOfWork, CommissionMembreRepository)
        {
            _unitOfWork = unitOfWork;
            _CommissionMembreRepository = CommissionMembreRepository;
        }


        public CommissionMembre GetById(int Id)
        {
            return _CommissionMembreRepository.GetById(Id);
        }

        /*Retourne la liste des commissionMembre by commission id*/

        public List<CommissionMembre> GetByIdCommission(int id)
        {
            return _CommissionMembreRepository.GetByIdCommission(id);
        }
    }
}

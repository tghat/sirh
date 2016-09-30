using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class MembreCommissionService : EntityService<MembreCommission>, IMembreCommissionService
    {
        IUnitOfWork _unitOfWork;
        IMembreCommissionRepository _MembreCommissionRepository;

        public MembreCommissionService(IUnitOfWork unitOfWork, IMembreCommissionRepository MembreCommissionRepository)
            : base(unitOfWork, MembreCommissionRepository)
        {
            _unitOfWork = unitOfWork;
            _MembreCommissionRepository = MembreCommissionRepository;
        }


        public MembreCommission GetById(int Id)
        {
            return _MembreCommissionRepository.GetById(Id);
        }

        public MembreCommission GetByNDoti(string Ndoti)
        {
            return _MembreCommissionRepository.GetByNDoti(Ndoti);
        }

        public MembreCommission GetByEmail(string Email)
        {
            return _MembreCommissionRepository.GetByEmail(Email);
        }

        public  List<MembreCommission> GetMembresByCommissionId(int id)
        {
            return _MembreCommissionRepository.GetMembresByCommissionId(id); 
        }
    }
}

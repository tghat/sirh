using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;

namespace ma.metl.sirh.Service
{
    public class AutorisationService : EntityService<AutorisationProfil>, IAutorisationService
    {
        IUnitOfWork _unitOfWork;
        IAutorisationServiceRepository _AutorisationRepository;
        
        public AutorisationService(IUnitOfWork unitOfWork, IAutorisationServiceRepository AutorisationRepository)
            : base(unitOfWork, AutorisationRepository)
        {
            _unitOfWork = unitOfWork;
            _AutorisationRepository = AutorisationRepository;
        }


        public AutorisationProfil GetById(int Id) {
            return _AutorisationRepository.GetById(Id);
        }

        public List<String> GetRolesByProfil(int ProfilId)
        {
            return _AutorisationRepository.GetRolesByProfil(ProfilId);
        }
       
    }
}

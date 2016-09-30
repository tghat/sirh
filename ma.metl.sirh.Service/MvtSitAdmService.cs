using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using ma.metl.sirh.Repository;
using ma.metl.sirh.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class MvtSitAdmService  : EntityGOService<MVT_SIT_ADM>, IMvtSitAdmService
    {
        IUnitOfWorkOrd _unitOfWork;
        IMvtSitAdmRepository _MvtSitAdmRepository;

        public MvtSitAdmService(IUnitOfWorkOrd unitOfWork, IMvtSitAdmRepository MvtSitAdmRepository)
            : base(unitOfWork, MvtSitAdmRepository)
        {
            _unitOfWork = unitOfWork;
            _MvtSitAdmRepository = MvtSitAdmRepository;
        }


        public List<SanctionDto> GetListSanctions(string numDoti)
        {
            return _MvtSitAdmRepository.GetListSanctions(numDoti);
        }
    }
}

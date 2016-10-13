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
        IUnitOfWork _unitOfWork;
        IMvtSitAdmRepository _mvtSitAdmRepository;

        public MvtSitAdmService(IUnitOfWork unitOfWork, IMvtSitAdmRepository mvtSitAdmRepository)
            : base(unitOfWork, mvtSitAdmRepository)
        {
            _unitOfWork = unitOfWork;
            _mvtSitAdmRepository = mvtSitAdmRepository;
        }


        public List<SanctionDto> GetListSanctions(string numDoti)
        {
            return _mvtSitAdmRepository.GetListSanctions(numDoti);
        }
    }
}

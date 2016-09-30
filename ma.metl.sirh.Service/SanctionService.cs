using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class SanctionService : EntityService<Sanction>, ISanctionService
    {
        IUnitOfWork _unitOfWork;
        ISanctionRepository _SanctionRepository;

        public SanctionService(IUnitOfWork unitOfWork, ISanctionRepository SanctionRepository)
            : base(unitOfWork, SanctionRepository)
        {
            _unitOfWork = unitOfWork;
            _SanctionRepository = SanctionRepository;
        }


        public Sanction GetById(int Id)
        {
            return _SanctionRepository.GetById(Id);
        }

        public List<Sanction> GetListSanctionByNumDoti(string numDoti)
        {
            return _SanctionRepository.GetListSanctionByNumDoti(numDoti);
        }
    }
}

using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using ma.metl.sirh.Repository;
using ma.metl.sirh.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace ma.metl.sirh.Service
{
    public class ActeGOService : EntityGOService<ACTE>, IActeGOService
    {
        IUnitOfWork _unitOfWork;
        IActeGORepository _acteGORepository;

        public ActeGOService(IUnitOfWork unitOfWork, IActeGORepository acteGORepository)
            : base(unitOfWork, acteGORepository)
        {
            _unitOfWork = unitOfWork;
            _acteGORepository = acteGORepository;
        }

        public ACTE GetLastActeByNumDotti(int numDoti)
        {
            return _acteGORepository.GetLastActeByNumDotti(numDoti);
        }
    }
}

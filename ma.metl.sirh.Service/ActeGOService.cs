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
    public class ActeGOService : EntityGOService<ACTE>, ma.metl.sirh.Service.IActeGOService
    {
        IUnitOfWorkOrd _unitOfWork;
        IActeGORepository _acteGORepository;

        public ActeGOService(IUnitOfWorkOrd unitOfWork, IActeGORepository acteGORepository)
            : base(unitOfWork, acteGORepository)
        {
            _unitOfWork = unitOfWork;
            _acteGORepository = acteGORepository;
        }

        public ACTE GetLastActeByNumDotti(int numDoti)
        {
            return _acteGORepository.GetLastActeByNumDotti(numDoti);
        }

        public List<ACTE_EVENT_HIST> GetActeEventsHistory(int NumDoti)
        {
            return _acteGORepository.GetActeEventsHistory(NumDoti);
        } 
    }
}

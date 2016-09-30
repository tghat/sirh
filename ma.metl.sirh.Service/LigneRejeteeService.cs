using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class LigneRejeteeService : EntityService<LigneRejetee>, ILigneRejeteeService
    {
        IUnitOfWork _unitOfWork;
        ILigneRejeteeRepository _LigneRejeteeRepository;

        public LigneRejeteeService(IUnitOfWork unitOfWork, ILigneRejeteeRepository LigneRejeteeRepository)
            : base(unitOfWork, LigneRejeteeRepository)
        {
            _unitOfWork = unitOfWork;
            _LigneRejeteeRepository = LigneRejeteeRepository;
        }
    }
}

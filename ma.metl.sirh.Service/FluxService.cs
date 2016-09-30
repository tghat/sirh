using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class FluxService : EntityService<Flux>, IFluxService
    {
        IUnitOfWork _unitOfWork;
        IFluxRepository _FluxRepository;

        public FluxService(IUnitOfWork unitOfWork, IFluxRepository FluxRepository)
            : base(unitOfWork, FluxRepository)
        {
            _unitOfWork = unitOfWork;
            _FluxRepository = FluxRepository;
        }


        public Flux GetById(int id)
        {
            return _FluxRepository.GetById(id);
        }
    }
}

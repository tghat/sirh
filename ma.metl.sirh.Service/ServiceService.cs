using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;

namespace ma.metl.sirh.Service
{
    public class ServiceService : EntityService<ma.metl.sirh.Model.Service>, IServiceService
    {
        IUnitOfWork _unitOfWork;
        IServiceRepository _ServiceRepository;

        public ServiceService(IUnitOfWork unitOfWork, IServiceRepository ServiceRepository)
            : base(unitOfWork, ServiceRepository)
        {
            _unitOfWork = unitOfWork;
            _ServiceRepository = ServiceRepository;
        }


        public ma.metl.sirh.Model.Service GetById(int Id)
        {
            return _ServiceRepository.GetById(Id);
        }

       
    }
}

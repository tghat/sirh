using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class LocaliteService : EntityService<Localite>, ILocaliteService
    {
        IUnitOfWork _unitOfWork;
        ILocaliteRepository _LocaliteRepository;

        public LocaliteService(IUnitOfWork unitOfWork, ILocaliteRepository LocaliteRepository)
            : base(unitOfWork, LocaliteRepository)
        {
            _unitOfWork = unitOfWork;
            _LocaliteRepository = LocaliteRepository;
        }


        public Localite GetById(int Id)
        {
            return _LocaliteRepository.GetById(Id);
        }

        public Localite GetByIdOrd(int Id)
        {
            return _LocaliteRepository.GetByIdOrd(Id);
        }
    }
}

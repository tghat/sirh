using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class SpecialiteService : EntityService<Specialite>, ISpecialiteService
    {
        IUnitOfWork _unitOfWork;
        ISpecialiteRepository _SpecialiteRepository;

        public SpecialiteService(IUnitOfWork unitOfWork, ISpecialiteRepository SpecialiteRepository)
            : base(unitOfWork, SpecialiteRepository)
        {
            _unitOfWork = unitOfWork;
            _SpecialiteRepository = SpecialiteRepository;
        }


        public Specialite GetById(int id)
        {
            return _SpecialiteRepository.GetById(id);
        }
    }
}

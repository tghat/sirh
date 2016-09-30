using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;

namespace ma.metl.sirh.Service
{
    public class DirectionService : EntityService<Direction>, IDirectionService
    {
        IUnitOfWork _unitOfWork;
        IDirectionRepository _DirectionRepository;

        public DirectionService(IUnitOfWork unitOfWork, IDirectionRepository DirectionRepository)
            : base(unitOfWork, DirectionRepository)
        {
            _unitOfWork = unitOfWork;
            _DirectionRepository = DirectionRepository;
        }


        public Direction GetById(int Id)
        {
            return _DirectionRepository.GetById(Id);
        }

        public Direction GetByCode(string code)
        {
            return _DirectionRepository.GetByCode(code);
        }

       
    }
}

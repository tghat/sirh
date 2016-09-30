using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class NotationService : EntityService<Notation>, INotationService
    {
        IUnitOfWork _unitOfWork;
        INotationRepository _NotationRepository;

        public NotationService(IUnitOfWork unitOfWork, INotationRepository NotationRepository)
            : base(unitOfWork, NotationRepository)
        {
            _unitOfWork = unitOfWork;
            _NotationRepository = NotationRepository;
        }


        public Notation GetById(int Id)
        {
            return _NotationRepository.GetById(Id);
        }

        public Notation GetByNotationByIdDetail(int idDetail)
        {
            return _NotationRepository.GetByNotationByIdDetail(idDetail);
        }
    }
}

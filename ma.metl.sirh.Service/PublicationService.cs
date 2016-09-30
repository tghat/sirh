using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class PublicationService : EntityService<Publication>, IPublicationService
    {

        IUnitOfWork _unitOfWork;
        IPublicationRepository _PublicationRepository;

        public PublicationService(IUnitOfWork unitOfWork, IPublicationRepository PublicationRepository)
            : base(unitOfWork, PublicationRepository)
        {
            _unitOfWork = unitOfWork;
            _PublicationRepository = PublicationRepository;
        }


        public Publication GetById(int Id)
        {
            return _PublicationRepository.GetById(Id);
        }

        public List<Publication> GetPublicationByCriteres(ProgrammeModel prog, string type)
        {
            return _PublicationRepository.GetPublicationByCriteres(prog, type);
        }

        public List<Publication> GetAllByType(string type)
        {
            return _PublicationRepository.GetAllByType(type);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;

namespace ma.metl.sirh.Service
{
    public class Autorisation : EntityService<Model.Autorisation>, IAutorisation
    {
        IUnitOfWork _unitOfWork;
        IAutorisationRepository _AutorisationRepository;
        
        public Autorisation(IUnitOfWork unitOfWork, IAutorisationRepository AutorisationRepository)
            : base(unitOfWork, AutorisationRepository)
        {
            _unitOfWork = unitOfWork;
            _AutorisationRepository = AutorisationRepository;
        }


        public Model.Autorisation GetById(int Id) {
            return _AutorisationRepository.GetById(Id);
        }

       
    }
}

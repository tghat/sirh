using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System.Data.Entity;

namespace ma.metl.sirh.Service
{
    public class ProfilService : EntityService<Profil>, IProfilService
    {
        IUnitOfWork _unitOfWork;
        IProfilRepository _ProfilRepository;

        public ProfilService(IUnitOfWork unitOfWork, IProfilRepository ProfilRepository)
            : base(unitOfWork, ProfilRepository)
        {
            _unitOfWork = unitOfWork;
            _ProfilRepository = ProfilRepository;
        }


        public Profil GetById(int Id)
        {
            return _ProfilRepository.GetById(Id);
        }

        public void Edit(Profil profil)
        {
            _ProfilRepository.Edit(profil); 
        }
    }
}

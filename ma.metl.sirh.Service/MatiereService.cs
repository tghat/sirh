using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class MatiereService : EntityService<Matiere>, IMatiereService
    {
        IUnitOfWork _unitOfWork;
        IMatiereRepository _MatiereRepository;

        public MatiereService(IUnitOfWork unitOfWork, IMatiereRepository MatiereRepository)
            : base(unitOfWork, MatiereRepository)
        {
            _unitOfWork = unitOfWork;
            _MatiereRepository = MatiereRepository;
        }


        public Matiere GetById(long Id)
        {
            return _MatiereRepository.GetById(Id);
        }

        public Matiere GetByLibelle(string libelle)
        {
            return _MatiereRepository.GetByLibelle(libelle);
        }

        public List<MatiereExamen> getListMatiereByExamen(long id)
        {
            return _MatiereRepository.getListMatiereByExamen(id);
        }
    }
}

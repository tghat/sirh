using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class HistoriqueService : EntityService<Historique>, IHistoriqueService
    {
        IUnitOfWork _unitOfWork;
        IHistoriqueRepository _HistoriqueRepository;

        public HistoriqueService(IUnitOfWork unitOfWork, IHistoriqueRepository HistoriqueRepository)
            : base(unitOfWork, HistoriqueRepository)
        {
            _unitOfWork = unitOfWork;
            _HistoriqueRepository = HistoriqueRepository;
        }


        public Historique GetById(int Id)
        {
            return _HistoriqueRepository.GetById(Id);
        }

        public List<Historique> GetByIdDetailAvancement(int id)
        {
            return _HistoriqueRepository.GetByIdDetailAvancement(id);
        }
        
    }
}

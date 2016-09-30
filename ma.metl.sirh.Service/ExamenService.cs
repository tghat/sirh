using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class ExamenService : EntityService<Examen>, IExamenService
    {
        IUnitOfWork _unitOfWork;
        IExamenRepository _ExamenRepository;

        public ExamenService(IUnitOfWork unitOfWork, IExamenRepository ExamenRepository)
            : base(unitOfWork, ExamenRepository)
        {
            _unitOfWork = unitOfWork;
            _ExamenRepository = ExamenRepository;
        }


        public Examen GetById(int Id)
        {
            return _ExamenRepository.GetById(Id);
        }

        public List<ResultatRapportDto> GetListExamen()
        {
            return _ExamenRepository.GetListExamen(); 
        }
        
        public Examen GetByAnneeAndIdGrade(string Annee, int idGrade)
        {
            return _ExamenRepository.GetByAnneeAndIdGrade(Annee, idGrade);
        }
    }
}

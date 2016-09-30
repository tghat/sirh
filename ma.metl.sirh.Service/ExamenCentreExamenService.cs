using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{

     public class ExamenCentreExamenService : EntityService<ExamenCentreExamen>, IExamenCentreExamenService
    {
        IUnitOfWork _unitOfWork;
        IExamenCentreExamenRepository _ExamenCentreExamenRepository;

        public ExamenCentreExamenService(IUnitOfWork unitOfWork, IExamenCentreExamenRepository ExamenCentreExamenRepository)
            : base(unitOfWork, ExamenCentreExamenRepository)
        {
            _unitOfWork = unitOfWork;
            _ExamenCentreExamenRepository = ExamenCentreExamenRepository;
        }


        public ExamenCentreExamen GetById(int Id)
        {
            return _ExamenCentreExamenRepository.GetById(Id);
        }

    }
}

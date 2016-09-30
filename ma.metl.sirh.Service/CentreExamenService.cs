using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class CentreExamenService : EntityService<CentreExamen>, ICentreExamenService
    {
        IUnitOfWork _unitOfWork;
        ICentreExamenRepository _CentreExamenRepository;

        public CentreExamenService(IUnitOfWork unitOfWork, ICentreExamenRepository CentreExamenRepository)
            : base(unitOfWork, CentreExamenRepository)
        {
            _unitOfWork = unitOfWork;
            _CentreExamenRepository = CentreExamenRepository;
        }


        public CentreExamen GetById(int Id)
        {
            return _CentreExamenRepository.GetById(Id);
        }

        public CentreExamen GetByName(string name)
        {
            return _CentreExamenRepository.GetByName(name); 
        }

        public List<CentreExamen> getListCentresByExamen(long id)
        {
            return _CentreExamenRepository.getListCentresByExamen(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;

namespace ma.metl.sirh.Service
{
    public class ModuleService : EntityService<Module>, IModuleService
    {
        IUnitOfWork _unitOfWork;
        IModuleRepository _ModuleRepository;

        public ModuleService(IUnitOfWork unitOfWork, IModuleRepository ModuleRepository)
            : base(unitOfWork, ModuleRepository)
        {
            _unitOfWork = unitOfWork;
            _ModuleRepository = ModuleRepository;
        }


        public Module GetById(int Id) {
            return _ModuleRepository.GetById(Id);
        }

       
    }
}

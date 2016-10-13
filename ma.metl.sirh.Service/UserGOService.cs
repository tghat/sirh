using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using ma.metl.sirh.Repository;
using ma.metl.sirh.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace ma.metl.sirh.Service
{
    public class UserGOService : EntityGOService<UTILISATEUR>, ma.metl.sirh.Service.IUserGOService
    {
        IUnitOfWork _unitOfWork;
        IUserGORepository _userGORepository;

        public UserGOService(IUnitOfWork unitOfWork, IUserGORepository userGORepository)
            : base(unitOfWork, userGORepository)
        {
            _unitOfWork = unitOfWork;
            _userGORepository = userGORepository;
        }

        public UTILISATEUR GetUserByCode(int code)
        {
            return _userGORepository.GetUserByCode(code);
        }

        
    }
}

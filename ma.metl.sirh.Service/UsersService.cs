using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;
using ma.metl.sirh.Repository;

namespace ma.metl.sirh.Service
{
    public class UsersService : EntityService<Users>, IUsersService
    {
        IUnitOfWork _unitOfWork;
        IUsersRepository _UsersRepository;
        
        public UsersService(IUnitOfWork unitOfWork, IUsersRepository UsersRepository)
            : base(unitOfWork, UsersRepository)
        {
            _unitOfWork = unitOfWork;
            _UsersRepository = UsersRepository;
        }


        public Users GetById(int Id) {
            return _UsersRepository.GetById(Id);
        }

        public Users GetByLogin(string login)
        {
            return _UsersRepository.GetByLogin(login);
        }

        public Boolean IsMailUnique(string Email)
        {
            return _UsersRepository.IsMailUnique(Email);
        }
    }
}

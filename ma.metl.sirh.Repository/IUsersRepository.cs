using System;
using ma.metl.sirh.Model;
namespace ma.metl.sirh.Repository
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Users GetById(int id);

        Users GetByLogin(string login);

        Boolean IsMailUnique(string Email);
    }
}

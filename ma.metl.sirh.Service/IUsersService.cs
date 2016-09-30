using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Service
{
    public interface IUsersService : IEntityService<Users>
    {
        Users GetById(int Id);

        Users GetByLogin(string login);

        Boolean IsMailUnique(string Email);
    }
}

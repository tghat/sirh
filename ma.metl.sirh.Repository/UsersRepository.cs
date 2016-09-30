using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Repository
{
  public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
      public UsersRepository(sirhContext context)
            : base(context)
        {
           
        }
        public Users GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();            
        }

        public Users GetByLogin(string login)
        {
            return FindBy(x => x.Login == login).FirstOrDefault();
        }

      public Boolean IsMailUnique(string Email)
        {
            if (FindBy(x => x.Email == Email).Count() != 0)
            {
                return true;
            }
            return false;
        }
    }
}

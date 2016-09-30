using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Repository
{
  public class AutorisationRepository : GenericRepository<Autorisation>, IAutorisationRepository
    {
      public AutorisationRepository(sirhContext context)
            : base(context)
        {
           
        }
        public Autorisation GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();            
        }

        
    }
}

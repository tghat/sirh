using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class ParametrageClassementRepository : GenericRepository<ParametrageClassement>, IParametrageClassementRepository
    {

        public ParametrageClassementRepository(sirhContext context)
            : base(context)
        {

        }
        
        public ParametrageClassement GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
    }
}

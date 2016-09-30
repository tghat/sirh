using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface IParametrageClassementRepository : IGenericRepository<ParametrageClassement>
    {
        ParametrageClassement GetById(int id); 
    }
}

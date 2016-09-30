using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IServiceService : IEntityService<ma.metl.sirh.Model.Service>
    {
        ma.metl.sirh.Model.Service GetById(int Id);

        
    }
}

using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IFluxService : IEntityService<Flux>
    {
        Flux GetById(int id);
    }
}

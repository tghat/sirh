using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Service
{
    public interface IDirectionService : IEntityService<Direction>
    {
        Direction GetById(int Id);

        Direction GetByCode(string code);
     
    }
}

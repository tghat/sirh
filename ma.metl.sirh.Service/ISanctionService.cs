using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface ISanctionService : IEntityService<Sanction>
    {
        Sanction GetById(int Id);
        List<Sanction> GetListSanctionByNumDoti(string numDoti);
    }
}

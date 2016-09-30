using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface ISanctionRepository : IGenericRepository<Sanction>
    {
        Sanction GetById(int id);
        List<Sanction> GetListSanctionByNumDoti(string numDoti);
    }
}

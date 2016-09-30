using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class LigneRejeteeRepository : GenericRepository<LigneRejetee>, ILigneRejeteeRepository
    {
        public LigneRejeteeRepository(sirhContext context)
            : base(context)
        {

        }
    }
}

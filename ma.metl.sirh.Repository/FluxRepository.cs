using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class FluxRepository : GenericRepository<Flux>, IFluxRepository
    {
        public FluxRepository(sirhContext context)
            : base(context)
        {

        }
        public Flux GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
    }
}

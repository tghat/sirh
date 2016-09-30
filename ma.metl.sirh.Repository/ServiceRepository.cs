using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Repository
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(sirhContext context)
            : base(context)
        {

        }
        public Service GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

    }
}

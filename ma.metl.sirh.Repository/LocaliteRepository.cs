using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class LocaliteRepository : GenericRepository<Localite>, ILocaliteRepository
    {
        public LocaliteRepository(sirhContext context)
            : base(context)
        {

        }
        public Localite GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public Localite GetByIdOrd(int id)
        {
            return FindBy(x => x.IdOrd == id).FirstOrDefault();
        }
    }
}

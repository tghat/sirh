using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class SpecialiteRepository : GenericRepository<Specialite>, ISpecialiteRepository
    {
        public SpecialiteRepository(sirhContext context)
            : base(context)
        {
           
        }
        public Specialite GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();            
        }
    }
}

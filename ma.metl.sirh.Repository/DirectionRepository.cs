using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Repository
{
    public class DirectionRepository : GenericRepository<Direction>, IDirectionRepository
    {
        public DirectionRepository(sirhContext context)
            : base(context)
        {

        }
        public Direction GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public Direction GetByCode(string code)
        {
            return FindBy(x => x.Code.Equals(code)).FirstOrDefault();
        }

    }
}

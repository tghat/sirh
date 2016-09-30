using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class NotationRepository : GenericRepository<Notation>, INotationRepository
    {
        public NotationRepository(sirhContext context)
            : base(context)
        {

        }
        public Notation GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public Notation GetByNotationByIdDetail(int idDetail)
        {
            var db = new sirhContext();
            var query = (from d in db.DetailAvancement
                         join n in db.Notation
                         on d.Id equals n.DetailAvancement.Id
                         where d.Id == idDetail
                         select n);
            return query.ToList().FirstOrDefault();
        }
    }
}

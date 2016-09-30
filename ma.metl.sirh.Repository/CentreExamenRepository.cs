using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class CentreExamenRepository: GenericRepository<CentreExamen>, ICentreExamenRepository
    {
        public CentreExamenRepository(sirhContext context)
            : base(context)
        {
           
        }
        public CentreExamen GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();            
        }

        public CentreExamen GetByName(string name)
        {
            return FindBy(x => x.name == name).FirstOrDefault();            
        }

        public List<CentreExamen> getListCentresByExamen(long id)
        {
            sirhContext db = new sirhContext();

            List<CentreExamen> query = (from x in db.CentreExamen
                                         join y in db.ExamenCentreExamen on x.Id equals y.CentreExamen_Id
                                         where y.Examen_Id == id
                                         select x).ToList();


            return query;
        }

    }
}

using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class MatiereRepository : GenericRepository<Matiere>, IMatiereRepository
    {
        public MatiereRepository(sirhContext context)
            : base(context)
        {
           
        }

        public Matiere GetById(long id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();            
        }

        public Matiere GetByLibelle(string libelle)
        {
            return FindBy(x => x.Intitule == libelle).FirstOrDefault();
        }

        public List<MatiereExamen> getListMatiereByExamen(long id)
        {            
            sirhContext db = new sirhContext();

            List<MatiereExamen> query = (from x in db.Matiere
                         join y in db.MatiereExamen on x.Id equals y.Matiere_Id
                         where y.Examen_Id == id
                         select y).ToList();


            return query;
        }
    }
}

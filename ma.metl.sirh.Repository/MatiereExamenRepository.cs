using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class MatiereExamenRepository : GenericRepository<MatiereExamen>, IMatiereExamenRepository
    {
        public MatiereExamenRepository(sirhContext context)
            : base(context)
        {

        }
        public MatiereExamen GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public MatiereExamen GetByIdMatiereIdExamen(long idMatiere, long idExamen)
        {
            return FindBy(x => x.Matiere_Id == idMatiere && x.Examen_Id == idExamen).FirstOrDefault();
        }
    }
}

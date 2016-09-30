using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class MembreCommissionRepository : GenericRepository<MembreCommission>, IMembreCommissionRepository
    {
        public MembreCommissionRepository(sirhContext context)
            : base(context)
        {

        }
        
        /* permet de récupérer le membre de commission by id*/

        public MembreCommission GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        /* permet de récupérer le membre de commission by N° Doti*/

        public MembreCommission GetByNDoti(string numDoti)
        {
            return FindBy(x => x.NDoti.Equals(numDoti)).FirstOrDefault();
        }


        public MembreCommission GetByEmail(string Email)
        {
            return FindBy(x => x.Email.Equals(Email)).FirstOrDefault();
        }

        public List<MembreCommission> GetMembresByCommissionId(int id)
        {
            sirhContext db = new sirhContext();

            List<MembreCommission> query = (from x in db.MembreCommission
                                         join y in db.CommissionMembre on x.Id equals y.MembreCap_Id
                                         where y.Commission_Id == id
                                         select x).ToList();


            return query;
        }
    }
}

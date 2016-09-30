using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class CommissionMembreRepository : GenericRepository<CommissionMembre>, ICommissionMembreRepository
    {
        public CommissionMembreRepository(sirhContext context)
            : base(context)
        {

        }

        public CommissionMembre GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public List<CommissionMembre> GetByIdCommission(int id)
        {
            return FindBy(x => x.Commission_Id == id).ToList(); 
        }
    }
}

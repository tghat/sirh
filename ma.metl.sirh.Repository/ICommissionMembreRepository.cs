using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface ICommissionMembreRepository : IGenericRepository<CommissionMembre>
    {
        CommissionMembre GetById(int id);
        List<CommissionMembre> GetByIdCommission(int id);

    }
}

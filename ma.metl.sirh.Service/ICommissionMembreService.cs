using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface ICommissionMembreService : IEntityService<CommissionMembre>
    {
        CommissionMembre GetById(int Id);
        List<CommissionMembre> GetByIdCommission(int id);
    }
}

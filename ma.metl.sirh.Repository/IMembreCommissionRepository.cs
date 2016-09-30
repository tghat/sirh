using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface IMembreCommissionRepository : IGenericRepository<MembreCommission>
    {
        MembreCommission GetById(int id);
        MembreCommission GetByNDoti(string NDoti);
        List<MembreCommission> GetMembresByCommissionId(int id);
        MembreCommission GetByEmail(string Email);
    }
}

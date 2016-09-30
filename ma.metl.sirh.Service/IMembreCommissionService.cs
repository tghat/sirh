using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IMembreCommissionService : IEntityService<MembreCommission>
    {
        MembreCommission GetById(int Id);
        MembreCommission GetByNDoti(string NDoti);
        List<MembreCommission> GetMembresByCommissionId(int id);
        MembreCommission GetByEmail(string Email);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Service
{
    public interface IAutorisationService : IEntityService<AutorisationProfil>
    {
        AutorisationProfil GetById(int Id);
        List<String> GetRolesByProfil(int ProfilId);
    }
}

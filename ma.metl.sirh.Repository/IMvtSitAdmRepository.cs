using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface IMvtSitAdmRepository : IGenericRepository<MVT_SIT_ADM>
    {
        List<SanctionDto> GetListSanctions(string numDoti);
    }
}

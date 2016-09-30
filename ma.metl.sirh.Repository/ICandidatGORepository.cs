using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface ICandidatGORepository : IGenericRepository<AGENT>
    {
        CandidatGODto GetCandidatFromGipeOrd(string NumDoti);
    }
}

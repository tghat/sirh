using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace ma.metl.sirh.Service
{
    public interface ICandidatGOService : IEntityGOService<AGENT>
    {

        CandidatGODto GetCandidatFromGipeOrd(string numDoti);
        CandidatDto GetCandidatByNumDoti(string numDoti);
        CandidatGODto GetCandidatFromGipeOrd(OdbcConnection connection, string numDoti);
        CandidatDto GetCandidatByNumDoti(OdbcConnection connection, string numDoti);
    }
}

using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
namespace ma.metl.sirh.Repository
{
    public interface IActeGORepository : IGenericRepository<ACTE>
    {
        ACTE GetLastActeByNumDotti(int NumDoti);
        List<ACTE_EVENT_HIST> GetActeEventsHistory(int NumDoti);
    }
}

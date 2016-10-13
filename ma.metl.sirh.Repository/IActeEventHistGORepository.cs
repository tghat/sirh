using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
namespace ma.metl.sirh.Repository
{
    public interface IActeEventHistGORepository : IGenericRepositoryOrd<ACTE_EVENT_HIST>
    {
        List<ACTE_EVENT_HIST> GetActeEventsHistory(int NumDoti);
    }
}

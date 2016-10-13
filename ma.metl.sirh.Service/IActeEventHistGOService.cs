using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
namespace ma.metl.sirh.Service
{
    public interface IActeEventHistGOService : IEntityGOService<ACTE_EVENT_HIST>
    {
        List<ACTE_EVENT_HIST> GetActeEventsHistory(int NumDoti);
        List<ACTE_EVENT_HIST> GetActeEventHistByCriteria(
            int? userCode,
            int? gradeId,
            string operationId,
            int? refArrete,
            DateTime? dateOperation);
    }
}

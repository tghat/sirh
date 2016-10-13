using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class ActeEventHistGORepository : GenericRepositoryOrd<GipeOrdContext, ACTE_EVENT_HIST>, IActeEventHistGORepository
    {
        public List<ACTE_EVENT_HIST> GetActeEventsHistory(int NumDoti)
        {
            var db1 = new GipeOrdContext();
            //int numDoti = Convert.ToInt32(NumDoti);
            var query = (from aeh in db1.ACTE_EVENT_HIST
                         orderby aeh.CREATION_DATE descending
                         where aeh.AGENT_CODE == NumDoti
                         select aeh).ToList();

            return query;
        }

        public ACTE_EVENT_HIST GetActeEventHistByCriteria() {
            return null;
        }
    }
}

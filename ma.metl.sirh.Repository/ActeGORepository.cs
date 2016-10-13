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
    public class ActeGORepository : GenericRepositoryOrd<GipeOrdContext,ACTE>, IActeGORepository
    {
        public ACTE GetLastActeByNumDotti(int NumDoti)
        {
            var db1 = new GipeOrdContext();
            //int numDoti = Convert.ToInt32(NumDoti);
            var query = (from aeh in db1.ACTE_EVENT_HIST
                         join acte in db1.ACTEs
                            on new { a = aeh.ACTE_REF_ARR, b = aeh.ACTE_ANN_VISA } equals new { a = acte.REF_ARRETE, b = acte.ANN_VISA }
                         orderby aeh.CREATION_DATE descending
                         where aeh.AGENT_CODE == NumDoti
                         select acte).FirstOrDefault();

            return query;
        }

         
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Repository;
using System.Diagnostics;
using System.IO;

namespace ma.metl.sirh.Service
{
    static class DateUtilsService
    {

        static public DateTime? addYear(DateTime startDate, int nbrAnnees)
        {
            DateTime? endDate = null;
            if (startDate != null)
            {
                endDate = startDate.AddYears(nbrAnnees);
            }
            return endDate;
        }
    }
}

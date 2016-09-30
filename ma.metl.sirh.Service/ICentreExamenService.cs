using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface ICentreExamenService : IEntityService<CentreExamen>
    {
        CentreExamen GetById(int Id);
        CentreExamen GetByName(string name);
        List<CentreExamen> getListCentresByExamen(long id);
    }
}

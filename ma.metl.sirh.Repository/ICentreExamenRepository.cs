using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface ICentreExamenRepository : IGenericRepository<CentreExamen>
    {
        CentreExamen GetById(int id);
        CentreExamen GetByName(string name);
        List<CentreExamen> getListCentresByExamen(long id);
    }
}

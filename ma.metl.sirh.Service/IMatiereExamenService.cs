using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IMatiereExamenService : IEntityService<MatiereExamen>
    {
        MatiereExamen GetById(int Id);

        MatiereExamen GetByIdMatiereIdExamen(long idMatiere, long idExamen);
    }
}

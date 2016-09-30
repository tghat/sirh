using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IMatiereService : IEntityService<Matiere>
    {
        Matiere GetById(long Id);
        Matiere GetByLibelle(string libelle);
        List<MatiereExamen> getListMatiereByExamen(long i);
    }
}

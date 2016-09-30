using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface IHistoriqueRepository : IGenericRepository<Historique>
    {
        Historique GetById(int id);
        List<Historique> GetByIdDetailAvancement(int id);
    }
}

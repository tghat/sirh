using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class HistoriqueRepository : GenericRepository<Historique>, IHistoriqueRepository
    {

        public HistoriqueRepository(sirhContext context)
            : base(context)
        {

        }
        public Historique GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
        public List<Historique> GetByIdDetailAvancement(int id)
        {
            return FindBy(x => x.DetailAvancement.Id == id).ToList(); 
        }
    }
}

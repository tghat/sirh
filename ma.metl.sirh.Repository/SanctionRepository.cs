using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class SanctionRepository : GenericRepository<Sanction>, ISanctionRepository
    {
        public SanctionRepository(sirhContext context)
            : base(context)
        {

        }
        public Sanction GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        //retourne la liste des sanctions par numdoti 

        public List<Sanction> GetListSanctionByNumDoti(string numDoti)
        {
            return FindBy(x => x.Candidat.NumDoti == numDoti).ToList(); 
        }
    }
}

using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class PublicationRepository : GenericRepository<Publication>, IPublicationRepository
    {
        public PublicationRepository(sirhContext context)
            : base(context)
        {

        }
        public Publication GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public List<Publication> GetAllByType(string type)
        {
            return FindBy(x => x.Type == type).ToList();
        }

        public List<Publication> GetPublicationByCriteres(ProgrammeModel prog, string type)
        {
            List<Publication> list = GetAllByType(type).ToList(); 
            if(prog.EtatPublication.ToString() != "Selectionnez")
            {
                list = list.Where(x => x.Statut == prog.EtatPublication).ToList(); 
            }
            if (prog.TypePublication.ToString() != "Selectionnez")
            {
                list = list.Where(x => x.TypePublication == prog.TypePublication.ToString()).ToList(); 
            }
            if(prog.DateDebut != null)
            {
                list = list.Where(x => x.DateDebutPub == prog.DateDebut).ToList(); 
            }
            if (prog.DateFin != null)
            {
                list = list.Where(x => x.DateFinPub == prog.DateFin).ToList();
            }
            return list;
        }
    }
}

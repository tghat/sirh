using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface IPublicationRepository : IGenericRepository<Publication>
    {
        Publication GetById(int id);
        List<Publication> GetPublicationByCriteres(ProgrammeModel prog, string type);
        List<Publication> GetAllByType(string type);
    }
}

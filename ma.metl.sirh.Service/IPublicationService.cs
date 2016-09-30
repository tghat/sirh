using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IPublicationService : IEntityService<Publication>
    {
        Publication GetById(int Id);
        List<Publication> GetPublicationByCriteres(ProgrammeModel prog, string type);
        List<Publication> GetAllByType(string type);
    }
}

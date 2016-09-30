using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface ILocaliteRepository : IGenericRepository<Localite>
    {
        Localite GetById(int id);
        Localite GetByIdOrd(int id);

    }
}

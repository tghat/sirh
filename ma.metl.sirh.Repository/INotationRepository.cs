using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface INotationRepository : IGenericRepository<Notation>
    {
        Notation GetById(int id);
        Notation GetByNotationByIdDetail(int idDetail);
    }
}

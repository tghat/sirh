using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface ICandidatureRepository : IGenericRepository<Candidature>
    {
        Candidature GetById(int id);
    }
}

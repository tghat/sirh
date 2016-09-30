using System;
using ma.metl.sirh.Model;
namespace ma.metl.sirh.Repository
{
    public interface IAutorisationRepository : IGenericRepository<Autorisation>
    {
        Autorisation GetById(int id);

       
    }
}

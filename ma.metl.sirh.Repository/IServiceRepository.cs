using System;
using ma.metl.sirh.Model;
namespace ma.metl.sirh.Repository
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Service GetById(int id);

        
    }
}

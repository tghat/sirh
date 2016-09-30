using System;
using ma.metl.sirh.Model;
namespace ma.metl.sirh.Repository
{
    public interface IModuleRepository : IGenericRepository<Module>
    {
        Module GetById(int id);

       
    }
}

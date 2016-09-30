using System;
using ma.metl.sirh.Model;
namespace ma.metl.sirh.Repository
{
    public interface IDirectionRepository : IGenericRepository<Direction>
    {
        Direction GetById(int id);

        Direction GetByCode(string code);

        
    }
}

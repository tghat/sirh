using System;
using ma.metl.sirh.Model;
namespace ma.metl.sirh.Repository
{
    public interface IProfilRepository : IGenericRepository<Profil>
    {
        Profil GetById(int id);
        void Edit(Profil profil);
        
    }
}

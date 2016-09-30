using System;
using ma.metl.sirh.Model;
using System.Collections.Generic;
namespace ma.metl.sirh.Repository
{
    public interface IAutorisationServiceRepository : IGenericRepository<AutorisationProfil>
    {
        AutorisationProfil GetById(int id);
        List<String> GetRolesByProfil(int profilId);     
    }
}

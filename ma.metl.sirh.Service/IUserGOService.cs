using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
namespace ma.metl.sirh.Service
{
    public interface IUserGOService : IEntityGOService<UTILISATEUR>
    {
        UTILISATEUR GetUserByCode(int code);
        List<UTILISATEUR> GetAllUsers();
    }
}

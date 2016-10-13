using ma.metl.sirh.Model.Ora;
using System;
namespace ma.metl.sirh.Service
{
    public interface IUserGOService : IEntityGOService<UTILISATEUR>
    {
        UTILISATEUR GetUserByCode(int code);
    }
}

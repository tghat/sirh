using ma.metl.sirh.Model.Ora;
using System;
namespace ma.metl.sirh.Repository
{
    public interface IUserGORepository : IGenericRepositoryOrd<UTILISATEUR>
    {
        UTILISATEUR GetUserByCode(int code);
    }
}

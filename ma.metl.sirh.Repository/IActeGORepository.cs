using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
namespace ma.metl.sirh.Repository
{
    public interface IActeGORepository : IGenericRepositoryOrd<ACTE>
    {
        ACTE GetLastActeByNumDotti(int NumDoti);
    }
}

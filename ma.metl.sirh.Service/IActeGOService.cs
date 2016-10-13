using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
namespace ma.metl.sirh.Service
{
    public interface IActeGOService : IEntityGOService<ACTE>
    {
        ACTE GetLastActeByNumDotti(int numDoti);
    }
}

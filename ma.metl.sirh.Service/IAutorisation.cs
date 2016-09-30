using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Service
{
    public interface IAutorisation : IEntityService<Model.Autorisation>
    {
        Model.Autorisation GetById(int Id);

    }
}

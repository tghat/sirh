﻿using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface ISpecialiteRepository : IGenericRepository<Specialite>
    {
        Specialite GetById(int id);

    }
}

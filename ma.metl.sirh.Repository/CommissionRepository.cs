﻿using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class CommissionRepository : GenericRepository<Commission>, ICommissionRepository
    {
        public CommissionRepository(sirhContext context)
            : base(context)
        {

        }
        public Commission GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }
    }
}

using System.Collections.Generic;
using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Ora;
using ma.metl.sirh.Model.Dto;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System;


namespace ma.metl.sirh.Repository
{
    public class CandidatureRepository : GenericRepository<Candidature>, ICandidatureRepository
    {
        public CandidatureRepository(sirhContext context)
            : base(context)
        {

        }

        public Candidature GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

    }  
}

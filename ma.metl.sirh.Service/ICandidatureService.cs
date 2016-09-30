using ma.metl.sirh.Model;
using System.Collections.Generic;
using ma.metl.sirh.Model.Dto;
using System;


namespace ma.metl.sirh.Service
{
    public interface ICandidatureService : IEntityService<Candidature>
    {
        Candidature GetById(int id);
        void creerCandidature(CandidatDto dto);
    }
}

using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Repository;
using System.Collections.Generic;
using System;
using AutoMapper;


namespace ma.metl.sirh.Service
{
    public class CandidatureService : EntityService<Candidature>, ICandidatureService
    {
        IUnitOfWork _unitOfWork;
        ICandidatureRepository _candidatureRepository;


        public CandidatureService(IUnitOfWork unitOfWork, ICandidatureRepository candidatureRepository)
            : base(unitOfWork, candidatureRepository)
        {
            _unitOfWork = unitOfWork;
            _candidatureRepository = candidatureRepository;
        }

        public Candidature GetById(int id)
        {
            return _candidatureRepository.GetById(id);
        }

        public void creerCandidature(CandidatDto dto) 
        {
            Candidature candidature = new Candidature();
            candidature.Annee = dto.AnneeProm;
            candidature.CandidatId = dto.Id;
            candidature.CreatedDate = DateTime.Now;
            candidature.UpdatedDate = DateTime.Now;
            candidature.GradeIdNouveau = dto.GradeId;
            candidature.Etat = EtatCandidature.Validee.ToString();
            Create(candidature);
        }
    }     
}

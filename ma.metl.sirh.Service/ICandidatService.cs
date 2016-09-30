using ma.metl.sirh.Model;
using System.Collections.Generic;
using ma.metl.sirh.Model.Dto;
using System;
using System.Data.Odbc;


namespace ma.metl.sirh.Service
{
    public interface ICandidatService : IEntityService<Candidat>
    {
        Candidat GetById(int id);
        Candidat GetByNumDoti(string numDoti);
        CandidatDto GetCandidatByNumDoti(string numDoti);
        List<CandidatResultatDto> GetAllCandidatAuChoix(CandidatCritereRechDto dto);
        List<CandidatResultatDto> GetAllCandidatSurExamen(string anneProm, int Ngrade);
        CandidatResultatDto GetSyntheseCandidat(int id);
        void creerCandidat(CandidatDto dto);
        void modifierCandidat(CandidatDto dto);
        List<CandidatResultatDto> GetAllCandidatExamen(CandidatCritereRechDto dto);
        CandidatDto GetCandidatById(int id);
        CandidatDto GetCandidatById(int id, int detailAvancementId);
        List<CandidatResultatDto> GetAllCandidatures(CandidatCritereRechDto dto);
        bool isExistingCandidat(string numDoti);
        CandidatDto GetCandidatByNumDotti(string numDotti, int detailAvancementId);
        List<CandidatResultatDto> GetAllCandidatAuChoix();
        CandidatDto GetCandidatByNumDotti(OdbcConnection connection, string numDotti, int detailAvancementId);
        void modifierCandidat(OdbcConnection connexion, CandidatDto dto);
        DetailAvancement creerCandidat(OdbcConnection connection, CandidatDto dto);
        List<CandidatResultatDto> GetCandidatAuChoixById(int id);
        List<CandidatResultatDto> GetAllCandidatSurExamen();
        DetailAvancement creerCandidatExamen(OdbcConnection connection, CandidatDto dto);
        DetailAvancement creerCandidatExamen(CandidatDto dto);
        List<CandidatResultatDto> GetCandidatExamenById(int id);
        List<InterfacageActeCandidatResultatVM> GetCandidatsByCriteria(string annee, long? gradeId, string statut);
    }
}

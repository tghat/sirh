using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using System.Collections.Generic;

namespace ma.metl.sirh.Service
{
    public interface IDetailAvancementService : IEntityService<DetailAvancement>
    {
        List<DetailAvancement> GetDetailByNumDoti(string numDoti);
        int GetNombreTotalPromouvableAC(int grade, string annee);
        int GetNombreTotalPromouvableAE(int grade, string annee);
        void modifyNoteMoyenne(string moyenne, string detailAvancement);
        void modifyStatut(string statut, long detailAvancementId);
        DetailAvancement GetById(int Id);
        void deleteAvancement(int id);
        void modifierDecisionCap(string decision, string motifDecision, long detailAvancementId);
        List<DetailAvancement> GetByGradeAnneeAC(int grade, string annee);
        List<CandidatResultatDto> GetByGradeAnneeAEEcrit(int grade, string annee);
        List<CandidatResultatDto> GetByGradeAnneeAEOral(int grade, string annee);
        List<CandidatResultatDto> GetByGradeAnneeAEGlobal(int grade, string annee);

    }
}

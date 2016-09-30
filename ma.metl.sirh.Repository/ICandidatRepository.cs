using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface ICandidatRepository : IGenericRepository<Candidat>
    {
        Candidat GetById(int id);
        Candidat GetByNumDoti(string numDoti);
        List<CandidatResultatDto> GetAllCandidatAuChoix(CandidatCritereRechDto dto, DateTime? AncienneteGrade, DateTime? AncienneteEchelon);
        List<CandidatResultatDto> GetAllCandidatSurExamen(string anneProm, int Ngrade);
        List<CandidatResultatDto> GetAllCandidatExamen(CandidatCritereRechDto dto, DateTime? AncienneteGrade, DateTime? AncienneteEchelon);
        List<CandidatResultatDto> GetAllCandidatures(CandidatCritereRechDto dto);
        bool isExistingCandidat(string numDoti, string annee);
        List<CandidatResultatDto> GetAllCandidatAuChoix();
        List<CandidatResultatDto> GetCandidatAuChoixById(int id);
        List<CandidatResultatDto> GetAllCandidatSurExamen();
        List<CandidatResultatDto> GetCandidatExamenById(int id);
        //List<InterfacageActeCandidatResultatVM> GetCandidatsByCriteria(string annee, long? gradeId, string statut);
    }
}

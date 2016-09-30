using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public class DetailAvancementService : EntityService<DetailAvancement>, IDetailAvancementService
    {
        IUnitOfWork _unitOfWork;
        IDetailAvancementRepository _DetailAvancementRepository;
        IHistoriqueService historiqueService;
        IHistoriqueRepository historiqueRepository;

        public DetailAvancementService(IUnitOfWork unitOfWork, IDetailAvancementRepository DetailAvancementRepository, IHistoriqueService historiqueService, IHistoriqueRepository historiqueRepository)
            : base(unitOfWork, DetailAvancementRepository)
        {
            _unitOfWork = unitOfWork;
            _DetailAvancementRepository = DetailAvancementRepository;
            this.historiqueService = historiqueService;
            this.historiqueRepository = historiqueRepository;
        }

        public List<DetailAvancement> GetDetailByNumDoti(string numDoti)
        {
            return _DetailAvancementRepository.GetDetailByNumDoti(numDoti);
        }

        public int GetNombreTotalPromouvableAC(int grade, string annee)
        {

            return _DetailAvancementRepository.GetNombreTotalPromouvableAC(grade, annee);
        }

        public int GetNombreTotalPromouvableAE(int grade, string annee)
        {

            return _DetailAvancementRepository.GetNombreTotalPromouvableAE(grade, annee);
        }

        public void modifyNoteMoyenne(string moyenne, string detailAvancement)
        {
            DetailAvancement detail = new DetailAvancement();
            detail = _DetailAvancementRepository.GetById(Int32.Parse(detailAvancement));
            detail.Note = decimal.Parse(moyenne.Replace(".", ","));
            _DetailAvancementRepository.Save();
        }

        public void modifyStatut(string statut, long detailAvancementId)
        {
            DetailAvancement detail = new DetailAvancement();
            detail = _DetailAvancementRepository.GetById((int)detailAvancementId);
            detail.Statut = statut;
            _DetailAvancementRepository.Save();
        }

        public void modifierDecisionCap(string decision, string motifDecision, long detailAvancementId)
        {
            DetailAvancement detail = new DetailAvancement();
            detail = _DetailAvancementRepository.GetById((int)detailAvancementId);
            detail.DecisionCap = decision;
            detail.motifDecision = motifDecision;
            _DetailAvancementRepository.Save();
        }

        public DetailAvancement GetById(int Id)
        {
            return _DetailAvancementRepository.GetById(Id);
        }

        public void deleteAvancement(int id) 
        {
            DetailAvancement detail = GetById(id);
            List<Historique> historiques = historiqueService.GetByIdDetailAvancement(id);
            foreach (var h in historiques)
            {
                historiqueRepository.Delete(h);
            }            
            Delete(detail);
        }

        public List<DetailAvancement> GetByGradeAnneeAC(int grade, string annee)
        {
            return _DetailAvancementRepository.GetByGradeAnneeAC(grade,annee);

        }

        public List<CandidatResultatDto> GetByGradeAnneeAEEcrit(int grade, string annee)
        {
            return _DetailAvancementRepository.GetByGradeAnneeAEEcrit(grade, annee);

        }

        public List<CandidatResultatDto> GetByGradeAnneeAEOral(int grade, string annee)
        {
            return _DetailAvancementRepository.GetByGradeAnneeAEOral(grade, annee);

        }

        public List<CandidatResultatDto> GetByGradeAnneeAEGlobal(int grade, string annee)
        {
            return _DetailAvancementRepository.GetByGradeAnneeAEGlobal(grade, annee);

        }
    }
}

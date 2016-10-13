using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using ma.metl.sirh.Repository;
using ma.metl.sirh.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace ma.metl.sirh.Service
{
    public class ActeEventHistGOService : EntityGOService<ACTE_EVENT_HIST>, IActeEventHistGOService
    {
        IUnitOfWork _unitOfWork;
        IActeEventHistGORepository _acteEventHistGORepository;
        IGradeRepository _gradeRepository;
        IGradeGORepository _gradeGORepository;

        public ActeEventHistGOService(
            IUnitOfWork unitOfWork, 
            IActeEventHistGORepository acteEventHistGORepository,
            IGradeRepository gradeRepository,
            IGradeGORepository gradeGORepository)
            : base(unitOfWork, acteEventHistGORepository)
        {
            _unitOfWork = unitOfWork;
            _acteEventHistGORepository = acteEventHistGORepository;
            _gradeRepository = gradeRepository;
            _gradeGORepository = gradeGORepository;
        }

        public List<ACTE_EVENT_HIST> GetActeEventsHistory(int NumDoti)
        {
            return _acteEventHistGORepository.GetActeEventsHistory(NumDoti);
        }

        public List<ACTE_EVENT_HIST> GetActeEventHistByCriteria(
            int? userCode, 
            int? gradeId, 
            string operationId, 
            int? refArrete, 
            DateTime? dateOperation)
        {
            var actes = (from a in _acteEventHistGORepository.GetAll()
                         select a).ToList();

            if (userCode != null)
                actes = actes.Where(x => x.CREATED_BY == userCode.Value).ToList();

            if (gradeId != null) {
                var gradeLocal = _gradeRepository.GetById(gradeId.Value);
                var gradeGo = new TAB_GRADE();
                if (gradeLocal != null) {
                    gradeGo = _gradeGORepository.FindBy(x => 
                        x.COD_CADRE == gradeLocal.CodeCadre && 
                        x.COD_CATEG == gradeLocal.CodeCateg &&
                        x.COD_CORPS == gradeLocal.CodeCorps &&
                        x.COD_GRADE == gradeLocal.CodeGrade).FirstOrDefault();
                }

                actes = actes.Where(x => 
                    x.GRADE_CADRE_COD == gradeGo.COD_CADRE &&
                    x.GRADE_CATEG_COD == gradeGo.COD_CATEG &&
                    x.GRADE_CORPS_COD == gradeGo.COD_CORPS &&
                    x.GRADE_GRADE_COD == gradeGo.COD_GRADE).ToList();
            }

            if (!string.IsNullOrEmpty(operationId))
                actes = actes.Where(x => x.ACTE_STADE == operationId).ToList();

            if (refArrete != null)
                actes = actes.Where(x => x.ACTE_REF_ARR == refArrete.Value).ToList();

            if (dateOperation != null)
                actes = actes.Where(x => x.CREATION_DATE.Value.ToShortDateString() == dateOperation.Value.ToShortDateString()).ToList();

            return actes;
        }
    }
}

using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class CommissionGradeRepository : GenericRepository<CommissionGrade>, ICommissionGradeRepository
    {
        IGradeRepository gradeRepo;

        public CommissionGradeRepository(sirhContext context, IGradeRepository gradeRepo)
            : base(context)
        {
            this.gradeRepo = gradeRepo; 
        }
        public CommissionGrade GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public List<CommissionGrade> GetListGradeByCommissionId(int id)
        {
            return FindBy(x => x.Commission_Id == id).ToList(); 
        }

        public List<Grade> GetListGrade(int idCom)
        {
            List<Grade> gradeList = gradeRepo.GetAll().ToList();
            List<CommissionGrade> comissionList = GetAll().Where(x => x.Commission_Id == idCom).ToList();
            List<Grade> query = (from gradeL in gradeList
                        join comissionL in comissionList on gradeL.Id equals comissionL.GradeId
                        select gradeL).ToList();
            return query;
        }

        public List<CommissionGrade> GetByCommissionId(int id)
        {
            return FindBy(x => x.Commission_Id == id).ToList();
        }
    }
}

using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface ICommissionGradeRepository : IGenericRepository<CommissionGrade>
    {
        CommissionGrade GetById(int id);

        List<CommissionGrade> GetListGradeByCommissionId(int id);

        List<CommissionGrade> GetByCommissionId(int id);

        List<Grade> GetListGrade(int idCom);
    }
}

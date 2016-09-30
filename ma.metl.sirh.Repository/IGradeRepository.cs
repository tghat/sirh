using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        Grade GetById(int id);
        Grade GetGradeByCode(string codeCateg, string codeCorps, string codeCadre, string codeGrade);
    }
}

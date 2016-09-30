using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IGradeService : IEntityService<Grade>
    {
        Grade GetById(int Id);
        Grade GetGradeByCode(string codeCateg, string codeCorps, string codeCadre, string codeGrade);
        GradeDto GetGradeDtoByCode(string codeCateg, string codeCorps, string codeCadre, string codeGrade);
    }
}

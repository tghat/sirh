using ma.metl.sirh.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public GradeRepository(sirhContext context)
            : base(context)
        {

        }
        public Grade GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public Grade GetGradeByCode(string codeCateg, string codeCorps, string codeCadre, string codeGrade)
        {
            return FindBy(x => x.CodeCateg.Equals(codeCateg) && x.CodeCorps.Equals(codeCorps) && x.CodeCadre.Equals(codeCadre) && x.CodeGrade.Equals(codeGrade)).FirstOrDefault();
        }
    }
}

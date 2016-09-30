using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class MvtSitAdmRepository : GenericRepository<MVT_SIT_ADM>, IMvtSitAdmRepository
    {
        public MvtSitAdmRepository(GipeOrdContext context)
            : base(context)
        {

        }
        public List<SanctionDto> GetListSanctions(string numDoti)
        {
            var db = new GipeOrdContext();
            int numdoti = Convert.ToInt32(numDoti);
            var query = (from sa in db.MVT_SIT_ADM
                         join ss in db.TAB_SS_TYP_MVT on sa.M_TYP_MVT equals ss.TYP_MVT  
                         where sa.M_SS_TYP_MVT == ss.SS_TYP_MVT && sa.M_TYP_MVT == "SA" && sa.M_COD_AG == numdoti
                         select new SanctionDto
                         {
                             Titre = ss.LIB_SS_TYP_MVT,
                             Reference = sa.M_REF_ARRETE,
                             DateEffet = sa.M_DAT_EFF_MVT,
                             AnneeActe = sa.M_ANN_VISA
                         }).Distinct();
             
            List<SanctionDto> list = query.ToList();
            return list;
        }
    }
}

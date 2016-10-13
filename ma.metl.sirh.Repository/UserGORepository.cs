using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Model.Ora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Repository
{
    public class UserGORepository : GenericRepositoryOrd<GipeOrdContext,UTILISATEUR>, ma.metl.sirh.Repository.IUserGORepository
    {
        public UTILISATEUR GetUserByCode(int code)
        {
            var db1 = new GipeOrdContext();
            //int numDoti = Convert.ToInt32(NumDoti);
            var query = db1.UTILISATEURS.FirstOrDefault(x => x.COD_USR == code);
            return query;
        }
    }
}

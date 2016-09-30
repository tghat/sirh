using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Repository
{
  public class AutorisationServiceRepository : GenericRepository<AutorisationProfil>, IAutorisationServiceRepository
    {
      public AutorisationServiceRepository(sirhContext context)
            : base(context)
        {
           
        }
        public AutorisationProfil GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();            
        }

        public List<String> GetRolesByProfil(int ProfilId) 
        {
          
            List<String> roles = new List<String>();
            var db = new sirhContext();
            var query = (from c in db.AutorisationProfils           
                         where c.Profil_Id == ProfilId         
                         select c.Autorisation.Code);
            roles = query.ToList();
            return roles;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ma.metl.sirh.Model;

namespace ma.metl.sirh.Repository
{
    public class ProfilRepository : GenericRepository<Profil>, IProfilRepository
    {
        public ProfilRepository(sirhContext context)
            : base(context)
        {

        }
        public Profil GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public void Edit(Profil profil)
        {
            sirhContext db = new sirhContext();
            db.Entry(profil).State = EntityState.Modified;
            db.SaveChanges();

        }

    }
}

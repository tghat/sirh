using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    public class sirhContext : DbContext
    {

        public sirhContext()
            : base("Name=sirhContext")
        {
            //this.Configuration.ValidateOnSaveEnabled = false;
            //this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Users> Users { get; set; }
      //  public DbSet<Country> Countries { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Profil> Profils { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Direction> Directions { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Service> Services { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Module> Modules { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Autorisation> Autorisations { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.AutorisationProfil> AutorisationProfils { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Candidat> Candidat { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Candidature> Candidature { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Flux> Flux { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.LigneRejetee> LigneRejetee { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.DetailAvancement> DetailAvancement { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Sanction> Sanction { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Parametre> Parametre { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.ParametrageQuota> ParametrageQuota { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Grade> Grade { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.ParametrageClassement> ParametrageClassement { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.CentreExamen> CentreExamen { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Examen> Examen { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.ExamenCentreExamen> ExamenCentreExamen { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Specialite> Specialite { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Matiere> Matiere { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.MatiereExamen> MatiereExamen { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Commission> Commission { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.MembreCommission> MembreCommission { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.CommissionMembre> CommissionMembre { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Reunion> Reunion { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Convocation> Convocation { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Historique> Historique { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Notation> Notation { get; set; }

        public System.Data.Entity.DbSet<ma.metl.sirh.Model.Publication> Publication { get; set; }


        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }

        

       
    }    
}

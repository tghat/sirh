using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FluentValidation.Mvc;
using FluentValidation;

namespace ma.metl.sirh.Model
{
    [Table("Module")]
    [FluentValidation.Attributes.Validator(typeof(ModuleValidator))]
    public class Module : AuditableEntity<long>
    {

       
        public string Code { get; set; }

        public string Description { get; set; }

    }

    public class ModuleValidator : AbstractValidator<Module>
    {
        public ModuleValidator()
        {
        //   RuleFor(x => x.Id).NotNull();
        }
    }

    [Table("Autorisation")]
    [FluentValidation.Attributes.Validator(typeof(AutorisationValidator))]
    public class Autorisation : AuditableEntity<long>
    {


        public string Code { get; set; }

        public string Description { get; set; }

        public long Module_Id { get; set; }
        [ForeignKey("Module_Id")]
        public virtual Module Module { get; set; }



    }
    public class AutorisationValidator : AbstractValidator<Autorisation>
    {
        public AutorisationValidator()
        {
            //   RuleFor(x => x.Id).NotNull();


        }
    }

    [Table("Ville")]
    [FluentValidation.Attributes.Validator(typeof(VilleValidator))]
    public class Ville : AuditableEntity<long>
    {


        public string Code { get; set; }

        public string Description { get; set; }

  
    }

    public class VilleValidator : AbstractValidator<Ville>
    {
        public VilleValidator()
        {
            //   RuleFor(x => x.Id).NotNull();


        }
    }

    [Table("Fonction")]
    [FluentValidation.Attributes.Validator(typeof(FonctionValidator))]
    public class Fonction : AuditableEntity<long>
    {


        public string Code { get; set; }

        public string Description { get; set; }


    }

    public class FonctionValidator : AbstractValidator<Fonction>
    {
        public FonctionValidator()
        {
            //   RuleFor(x => x.Id).NotNull();


        }
    }
    [Table("AutorisationProfil")]
    [FluentValidation.Attributes.Validator(typeof(AutorisationProfilValidator))]
    public class AutorisationProfil : AuditableEntity<long>
    {
        public long Autorisation_Id { get; set; }
        [ForeignKey("Autorisation_Id")]
        public virtual Autorisation Autorisation { get; set; }

        public long Profil_Id { get; set; }
        [ForeignKey("Profil_Id")]
        public virtual Profil Profil { get; set; }



    }
    public class AutorisationProfilValidator : AbstractValidator<AutorisationProfil>
    {
        public AutorisationProfilValidator()
        {
            //   RuleFor(x => x.Id).NotNull();


        }
    }

    public class AutorisationService
    {
        public Profil Profil { get; set; }
        public AutorisationProfil AutorisationProfil { get; set; }
    }

    [Table("ExamenCentreExamen")]
    public class ExamenCentreExamen : AuditableEntity<long>
    {
        public long Examen_Id { get; set; }
        [ForeignKey("Examen_Id")]
        public virtual Examen Examen { get; set; }

        public long CentreExamen_Id { get; set; }
        [ForeignKey("CentreExamen_Id")]
        public virtual CentreExamen CentreExamen { get; set; }



    }

}

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
    [Table("Users")]
    [FluentValidation.Attributes.Validator(typeof(UsersValidator))]
    public class Users : AuditableEntity<long>
    {

        
        public string Name { get; set; }

        
        public string Prenom { get; set; }

        
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

       
        public long ProfilId { get; set; }
        [ForeignKey("ProfilId")]
        public virtual Profil Profil { get; set; }
       
        public long DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public virtual Direction Direction { get; set; }

        public long ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        
       
        public string Identifiant { get; set; }


        public string Statut { get; set; }

      
        
        public string Login { get; set; }

       
        public string Password { get; set; }

       

        //[Required]
        //[MaxLength(100)]
        //[Compare("Password", ErrorMessage =
        //"Le mot de passe ne correspond pas au mot de passe défini.")]
        public string ConfirmationPassword { get; set; }
        }

    public class UsersValidator : AbstractValidator<Users>
    {
        public UsersValidator()
        {
            
            
      
            
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Email).NotEmpty().WithMessage("Le champ Email est obligatoire");
            //RuleFor(x => x.Email).Must(UniqueMail).WithMessage("L'adresse e-mail existe déjà");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Le champ mot de passe est obligatoire");
            RuleFor(x => x.ConfirmationPassword).NotEmpty().WithMessage("Confirmer votre mot de passe");
            RuleFor(x => x.ConfirmationPassword).Equal(x => x.Password).WithMessage("Le mot de passe et la confirmation du mot de passe doivent être identiques");
            RuleFor(x => x.DirectionId).NotEmpty().WithMessage("Le champ direction est obligatoire");
            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Le champ service est obligatoire");
            RuleFor(x => x.ProfilId).NotEmpty().WithMessage("Le champ profil est obligatoire");

        }
             private bool UniqueMail(string Mail)
            {
                 Boolean var = false;
                 sirhContext db=new sirhContext();
                 if (db.Users.Where(x=>x.Email==Mail) == null)
                    {
                        var = true;
                    }
                    return var;
                 }
           
   }
}


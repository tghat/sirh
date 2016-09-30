using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace ma.metl.sirh.Model
{
    [Table("Profil")]
    [FluentValidation.Attributes.Validator(typeof(ProfilValidator))]
    public class Profil : AuditableEntity<long>
    {
        public string Code { get; set; }
        public string Libelle { get; set; }
        public string Role { get; set; }
    }

    public class ProfilValidator : AbstractValidator<Profil>
    {
        public ProfilValidator()
        {
            RuleFor(x => x.Libelle).NotEmpty();

        }
    }
}


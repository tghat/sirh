using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Matiere")]
    [FluentValidation.Attributes.Validator(typeof(MatiereValidator))]
    public class Matiere : AuditableEntity<long>
    {
        public string Intitule { get; set; }

        public virtual List<MatiereExamen> ListMatiereExamen { get; set; }

    }

    public class MatiereValidator : AbstractValidator<Matiere>
    {
        public MatiereValidator()
        {
            RuleFor(x => x.Intitule).NotNull().WithMessage("Le champ intitulé est obligatoire!");
           

        }
    }
}

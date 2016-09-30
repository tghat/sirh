using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("CentreExamen")]
    [FluentValidation.Attributes.Validator(typeof(CentreExamenValidator))]
    public class CentreExamen : AuditableEntity<long>
    {
        public string name { get; set; }

        public string adresse { get; set; }

        public long LocaliteId { get; set; }
        [ForeignKey("LocaliteId")]
        public virtual Localite Localite { get; set; }

    }

    public class CentreExamenValidator : AbstractValidator<CentreExamen>
    {
        public CentreExamenValidator()
        {
            RuleFor(x => x.name).NotNull().WithMessage("Le champ intitulé est obligatoire!");
            RuleFor(x => x.LocaliteId).NotNull().WithMessage("Le champ Localité est obligatoire!");
           
        }
    }
}

using FluentValidation;
using ma.metl.sirh.Model.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("ParametrageClassement")]
    [FluentValidation.Attributes.Validator(typeof(ParametrageClassementValidator))]
    public class ParametrageClassement : AuditableEntity<long>
    {
        public string Critere { get; set; }

        [RegularExpression(@"-?(?:\d*[\,\.])?\d+",ErrorMessageResourceName = "Invalid",ErrorMessageResourceType = typeof(Resources))]
        public decimal Valeur { get; set; }

        public string Annee { get; set; }

        public string TypeFlux { get; set; }

        public long? GradeId { get; set; }
        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        public class ParametrageClassementValidator : AbstractValidator<ParametrageClassement>
        {
            public ParametrageClassementValidator()
            {
                RuleFor(x => x.Critere).NotEmpty().WithMessage("Le champ critère est obligatoire!");
                RuleFor(x => x.Valeur).NotEmpty().WithMessage("Le champ valeur est obligatoire");


            }

        }
    }
}

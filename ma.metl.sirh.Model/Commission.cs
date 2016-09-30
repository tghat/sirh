using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Commission")]
    [FluentValidation.Attributes.Validator(typeof(CommissionValidator))]
    public class Commission : AuditableEntity<long>
    {
        public string Titre { get; set; }

        public string Annee { get; set; }

        public string EcritOrOral { get; set; }

        public string TypeAvancement { get; set; }

        public long GradeId { get; set; }
        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }


    }
    public class CommissionValidator : AbstractValidator<Commission>
    {
        public CommissionValidator()
        {
            RuleFor(x => x.Titre).NotEmpty().WithMessage("Le champ Titre est obligatoire!");
            RuleFor(x => x.Annee).NotEmpty().WithMessage("Le champ Année est obligatoire!");
            RuleFor(x => x.GradeId).NotNull().WithMessage("Le champ Grade est obligatoire!");
            RuleFor(x => x.TypeAvancement).NotNull().WithMessage("Le champ Type d'avancement est obligatoire!");
        }
    }
}

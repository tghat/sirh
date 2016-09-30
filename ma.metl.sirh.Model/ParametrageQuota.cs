using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("ParametrageQuota")]
    [FluentValidation.Attributes.Validator(typeof(ParametrageQuotaValidator))]
    public class ParametrageQuota : AuditableEntity<long>
    {
        public long? GradeIdAcces { get; set; }
        [ForeignKey("GradeIdAcces")]
        public virtual Grade GradeAcces { get; set; }

        public long? GradeIdOccupe { get; set; }
        [ForeignKey("GradeIdOccupe")]
        public virtual Grade GradeOccupe { get; set; }

        public string Anciennete { get; set; }

        public string Annee { get; set; }

        public decimal Quota { get; set; }

        //Nombre de poste à ouvrir
        public decimal NbrPoste { get; set; }

        public string TypeFlux { get; set; }

        public string Statut { get; set; }

        public string Commentaire { get; set; }
    }

    public class ParametrageQuotaValidator : AbstractValidator<ParametrageQuota>
    {
        public ParametrageQuotaValidator()
        {
            RuleFor(x => x.Annee).NotNull().WithMessage("Le champ Année est obligatoire!");
            RuleFor(x => x.GradeIdAcces).NotNull().WithMessage("Le champ Grade d'accès est obligatoire!");
            RuleFor(x => x.GradeIdOccupe).NotNull().WithMessage("Le champ Grade occupé est obligatoire!");
            RuleFor(x => x.NbrPoste).NotEmpty().WithMessage("Le champ nombre de poste est obligatoire!");
            RuleFor(x => x.Quota).NotEmpty().WithMessage("Le champ quota est obligatoire!");
        }
    }
}

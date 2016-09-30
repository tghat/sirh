using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Flux")]
    [FluentValidation.Attributes.Validator(typeof(FluxValidator))]
    public class Flux : AuditableEntity<long>
    {
        public string TypeFlux { get; set; }
        public string Source { get; set; }
        public int nbrTotalLigne { get; set; }
        public int nbrLigneRejete { get; set; }
        public string flux { get; set; }
        public string name { get; set; } 
        public DateTime dateIntegration { get; set; }
        public string anneeReception { get; set; }
        public string Etat { get; set; }
        public virtual List<DetailAvancement> ListDetailAvancement { get; set; }
        public virtual List<LigneRejetee> LignesRejetees { get; set; }
    }
    public class FluxValidator : AbstractValidator<Flux>
    {
        public FluxValidator()
        {
            RuleFor(x => x.TypeFlux).NotEqual("Selectionnez").WithMessage("Le champ type flux est obligatoire!");
            RuleFor(x => x.flux).NotEmpty().WithMessage("Le champ flux est obligatoire!");
        }
      
    }
}

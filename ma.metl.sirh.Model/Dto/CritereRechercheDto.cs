using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model.Dto
{
    [FluentValidation.Attributes.Validator(typeof(CritereRechercheDtoValidator))]
    public class CritereRechercheDto
    {
        public string AnneeProm { get; set; }
        public int GradeId { get; set; }
        public int ExamenId { get; set; }
        public DateTime DateExamen { get; set; } 
    }

    public class CritereRechercheDtoValidator : AbstractValidator<CritereRechercheDto>
    {
        public CritereRechercheDtoValidator()
        {
            RuleFor(x => x.AnneeProm).NotEmpty().WithMessage("Le champ année est obligatoire!");
            RuleFor(x => x.GradeId).NotEmpty().WithMessage("Le champ Grade est obligatoire!");
            RuleFor(x => x.ExamenId).NotEmpty().WithMessage("Le champ examen est obligatoire!");
            RuleFor(x => x.DateExamen).NotEmpty().WithMessage("Le champ date examen est obligatoire!");
        }
    }
}

using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model.Dto
{
    [FluentValidation.Attributes.Validator(typeof(CandidatDtoValidator))]
    public class CandidatDto
    {
        public long Id { get; set; }
        public string NumDoti { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string TelPersonnel { get; set; }
        public string Sexe { get; set; }
        public string CIN { get; set; }
        public string decision { get; set; }
        public string motifDecision { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DateNaissance { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DateRecrutement { get; set; }

        public string AncienGrade { get; set; }
        public DateTime? DateEffet { get; set; }
        public string Direction { get; set; }
        public int RegionId { get; set; }
        public int GradeId { get; set; }
        public int ExamenId { get; set; }
        public string AnneeProm { get; set; }
        public String CodeGrade { get; set; }
        public String CodeCateg { get; set; }
        public String CodeCorps { get; set; }
        public String CodeCadre { get; set; }

        public long detailAvancementId { get; set; }
        public long? ancienGradeId { get; set; }
        public long? nouveauGradeId { get; set; }

        public Convocation Convocation { get; set; }

        public class CandidatDtoValidator : AbstractValidator<CandidatDto>
        {
            public CandidatDtoValidator()
            {
                RuleFor(x => x.NumDoti).NotEmpty().WithMessage("Le champ N°Dotti est obligatoire");
                RuleFor(x => x.decision).NotEmpty().WithMessage("Le champ Décision est obligatoire");
                RuleFor(x => x.motifDecision).NotEmpty().WithMessage("Le champ motif de report est obligatoire");
                RuleFor(x => x.GradeId).NotEmpty().WithMessage("Le champ Grade est obligatoire");
                RuleFor(x => x.ExamenId).NotEmpty().WithMessage("Le champ Examen est obligatoire");
                RuleFor(x => x.AnneeProm).NotEmpty().WithMessage("Le champ Année est obligatoire");
                RuleFor(x => x.RegionId).NotEmpty().WithMessage("Le champ Region est obligatoire");
                RuleFor(x => x.Nom).NotEmpty().WithMessage("Le champ Nom est obligatoire");
                RuleFor(x => x.Prenom).NotEmpty().WithMessage("Le champ Prénom est obligatoire");
                RuleFor(x => x.CIN).NotEmpty().WithMessage("Le champ CIN est obligatoire");
                RuleFor(x => x.DateNaissance).NotEmpty().WithMessage("Le champ Date Naissance est obligatoire");
                RuleFor(x => x.DateRecrutement).NotEmpty().WithMessage("Le champ Date Recrutement est obligatoire");
            }    
        }
    }
}

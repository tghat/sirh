using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("DetailAvancement")]
    public class DetailAvancement : AuditableEntity<long>
    {
        public string Annee { get; set; }
        public DateTime DateAncienGrade { get; set; }
        public DateTime DateEff { get; set; }
        public decimal Note { get; set; }
        public string Statut { get; set;}
        public string DecisionCap { get; set; }
        public string motifDecision { get; set; }
        public DateTime AncienneteGrade { get; set; }
        public DateTime AncienneteEchelon { get; set; }

        public long CandidatId { get; set; }
        [ForeignKey("CandidatId")]
        public virtual Candidat Candidat { get; set; }

        public long FluxId { get; set; }
        [ForeignKey("FluxId")]
        public virtual Flux Flux { get; set; }

        public long? GradeIdAncien { get; set; }
        [ForeignKey("GradeIdAncien")]
        public virtual Grade AncienGrade { get; set; }

        public long? GradeIdNouveau { get; set; }
        [ForeignKey("GradeIdNouveau")]
        public virtual Grade NouveauGrade { get; set; }

    }
}

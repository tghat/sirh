using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Candidature")]
    public class Candidature: AuditableEntity<long>
    {

        public string Etat { get; set; }
        public string Formulaire { get; set; }
        public string Annee { get; set; }

        public long? CentreId { get; set; }
        [ForeignKey("CentreId")]
        public virtual CentreExamen CentreExamen { get; set; }

        public long CandidatId { get; set; }
        [ForeignKey("CandidatId")]
        public virtual Candidat Candidat { get; set; }

        public long? GradeIdNouveau { get; set; }
        [ForeignKey("GradeIdNouveau")]
        public virtual Grade NouveauGrade { get; set; }
    }
}

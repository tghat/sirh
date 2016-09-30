using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Examen")]
    public class Examen : AuditableEntity<long>
    {
        public string Intitule { get; set; }

        public long GradeId { get; set; }
        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        public long DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public virtual Direction Direction { get; set; }

        public decimal NoteEliminatoire { get; set; }

        public decimal MoyennePassage { get; set; }

        public string Annee { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Datelimite { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DateExamen { get; set; }

        public int nbrCandidatEligibleAnnee { get; set; }

        public int nbrCandidatEligibleDateExam { get; set; }

        public virtual List<MatiereExamen> ListMatiereExamen { get; set; }

    }
}

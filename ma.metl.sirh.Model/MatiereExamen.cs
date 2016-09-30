using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("MatiereExamen")]
    public class MatiereExamen :AuditableEntity<long>
    {
        public long Examen_Id { get; set; }
        [ForeignKey("Examen_Id")]
        public virtual Examen Examen { get; set; }

        public long Matiere_Id { get; set; }
        [ForeignKey("Matiere_Id")]
        public virtual Matiere Matiere { get; set; }

        public int Coefficient { get; set; }

        public DateTime? DateMatiere { get; set; }

        public string DureeH { get; set; }

        public string DureeM { get; set; }

        public string HeureDebutH { get; set; }

        public string HeureDebutM { get; set; }
    }
}

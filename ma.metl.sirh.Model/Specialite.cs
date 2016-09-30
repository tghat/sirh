using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Specialite")]
    public class Specialite : AuditableEntity<long>
    {
        public string Intitule { get; set; }

        public long? GradeId { get; set; }
        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        public long ExamenId { get; set; }
        [ForeignKey("ExamenId")]
        public virtual Examen Examen { get; set; }
    }
}

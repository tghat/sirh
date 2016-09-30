using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("MembreCommission")]
    public class MembreCommission : AuditableEntity<long>
    {
        public string NDoti { get; set; }

        public string nom { get; set; }

        public string nomA { get; set; }

        public string prenom { get; set; }

        public string prenomA { get; set; }

        public long DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public virtual Direction Direction { get; set; }

        public long? GradeId { get; set; }
        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        public long LocaliteId { get; set; }
        [ForeignKey("LocaliteId")]
        public virtual Localite Localite { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public string Adresse { get; set; }

        public string Interim { get; set; }
    }
}

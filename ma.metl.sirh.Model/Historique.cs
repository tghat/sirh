using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Historique")]
    public class Historique : AuditableEntity<long>
    {

        public string Action { get; set; }

        public string Message { get; set; }

        public DateTime DateAction { get; set; }

        public string NomComplet { get; set; }

        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        public long? DetailId { get; set; }
        [ForeignKey("DetailId")]
        public virtual DetailAvancement DetailAvancement { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("LigneRejetee")]
    public class LigneRejetee : AuditableEntity<long>
    {
        public string detail { get; set; }
        public string motifRejet { get; set; }
    }
}

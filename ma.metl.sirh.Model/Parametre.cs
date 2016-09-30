using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Parametre")]
    public class Parametre : AuditableEntity<long>
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public string TypeParametre { get; set; }
    }
}

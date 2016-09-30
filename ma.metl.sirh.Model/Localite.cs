using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Localite")]
    public class Localite : AuditableEntity<long>
    {
        public string Intitule { get; set; }

        public string IntituleA { get; set; }

        public int IdOrd { get; set; }
    }
}

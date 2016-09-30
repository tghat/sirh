using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Convocation")]
    public class Convocation : AuditableEntity<long>
    {
        public string Objet { get; set; }

        public string Message { get; set; }

        public string ConvocationFile { get; set; }

        public long MembreCap_Id { get; set; }
        [ForeignKey("MembreCap_Id")]
        public virtual MembreCommission MembreCommission { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("CommissionMembre")]
    public class CommissionMembre : AuditableEntity<long>
    {

        public long Commission_Id { get; set; }
        [ForeignKey("Commission_Id")]
        public virtual Commission Commission { get; set; }

        public long MembreCap_Id { get; set; }
        [ForeignKey("MembreCap_Id")]
        public virtual MembreCommission MembreCommission { get; set; }
    }
}

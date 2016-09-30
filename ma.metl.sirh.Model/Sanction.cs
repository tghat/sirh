using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Sanction")]
    public class Sanction : AuditableEntity<long>
    {
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DateEffet { get; set; }
        public int? Reference { get; set; }
        public string AnneeActe { get; set; }

        public long CandidatId { get; set; }
        [ForeignKey("CandidatId")]
        public virtual Candidat Candidat { get; set; }
    }
}

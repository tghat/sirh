using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Notation")]
    public class Notation :AuditableEntity<long>
    {

        public decimal NoteEcrite { get; set; }
        public decimal NotePresConnaissanceG { get; set; }
        public decimal NoteConnaissMin { get; set; }
        public decimal NoteConnaissSp { get; set; }
        public decimal NoteOrale { get; set; }
        public decimal NoteGlobale { get; set; }

        public DetailAvancement DetailAvancement { get; set; }

    }
}

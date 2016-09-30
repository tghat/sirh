using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Reunion")]
    public class Reunion :AuditableEntity<long>
    {
        public string OrdreJour { get; set; }

        public string OrdreJourAr { get; set; }

        public string Observation { get; set; }

        public string Decisions { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateReunion { get; set; }

        public string Lieu { get; set; }

        public string Pv { get; set; }

        public string DureeString { get; set; }

        public int Duree { get; set; }

        public long CommissionId { get; set; }
        [ForeignKey("CommissionId")]
        public virtual Commission Commission { get; set; }

        public string StatusENUM { get; set; }

    }
}

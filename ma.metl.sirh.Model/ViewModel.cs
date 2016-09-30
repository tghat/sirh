using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    public class ViewModel
    {
        public List<Commission> ListCommission { get; set; }
        public Commission Commission { get; set; }
        public List<Reunion> ListReunion { get; set; }
        public Reunion Reunion { get; set; }
        public List<MembreCommission> ListMembre { get; set; }
        public MembreCommission MembreCommission { get; set; }
        public Convocation Convocation { get; set; }

    }
}

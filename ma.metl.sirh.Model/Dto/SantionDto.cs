using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model.Dto
{
    public class SanctionDto
    {
        public string Titre { get; set; }
        public DateTime? DateEffet { get; set; }
        public int? Reference { get; set; }
        public short? AnneeActe { get; set; }
    }
}

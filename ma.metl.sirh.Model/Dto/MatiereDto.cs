using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ma.metl.sirh.Model.Dto
{
    public class MatiereDto
    {
        public long IdMatiere { get; set; }
        public long IdExamen { get; set; }
        public string Intitule { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Date { get; set; }
        public int Coefficient { get; set; }
        public string DureeH { get; set; }
        public string DureeM { get; set; }
        public string HeureDebutH { get; set; }
        public string HeureDebutM { get; set; }
        public string JourDateMatiere { get; set; }

        public IEnumerable<SelectListItem> ListDureeH { get; set; }
        public IEnumerable<SelectListItem> ListDureeM { get; set; }
        public IEnumerable<SelectListItem> ListHeureDebutH { get; set; }
        public IEnumerable<SelectListItem> ListHeureDebutM { get; set; }
    }
}

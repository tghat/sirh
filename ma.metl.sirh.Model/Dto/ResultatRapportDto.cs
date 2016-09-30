using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model.Dto
{
    public class ResultatRapportDto
    {
        public string IntituleGrade { get; set; }
        public int nbrTotalannee { get; set; }
        public int nbrTotalDate { get; set; }
        public string AnneeExam { get; set; }
        public string DateExamenString { get; set; }
        public DateTime? DateExamen { get; set; }
        public int nbrRestant { get; set; }
        public decimal pourcentageRestant { get; set; }
        public decimal pourcentageDateExamen { get; set; }
        public string Formule { get; set; }
        public string Remarque { get; set; }
        public int NbrPosteOuvrir { get; set; }
    }
}

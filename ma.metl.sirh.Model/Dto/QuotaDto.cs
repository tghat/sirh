using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ma.metl.sirh.Model.Dto
{
    public class QuotaDto
    {
        public long id { get; set; }
        public string Grade { get; set; }
        public string GradeA { get; set; }
        public decimal Quota { get; set; }
        public decimal NbrPosteOuvrir { get; set; }
        public string Commentaire { get; set; }
        public SelectList Statut { get; set; }
        public string StatutTQ { get; set; }
        public string Annee { get; set; }
    }
}

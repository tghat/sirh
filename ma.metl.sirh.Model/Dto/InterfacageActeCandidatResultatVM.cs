using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model.Dto
{
    public class InterfacageActeCandidatResultatVM
    {
        public long CandidatId { get; set; }
        public long DetailAvancementId { get; set; }
        public string NumDoti { get; set; }
        public string CandidatNom { get; set; }
        public string CandidatPrenom { get; set; }
        public string DeatilAvancementStatut { get; set; }
        public string Annee { get; set; }
        public long? GradeId { get; set; }
        public string Statut { get; set; }
        public bool Selected { get; set; }
    }
}

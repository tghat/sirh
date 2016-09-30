using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model.Dto
{
    public class CandidatGODto
    {
        public long Id { get; set; }
        public String NumDoti { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String NomAR { get; set; }
        public String PrenomAR { get; set; }
        public short? AnnDateNaissance { get; set; }
        public short? MoiDateNaissance { get; set; }
        public short? JourDateNaissance { get; set; }
        public DateTime? DateRecrutement { get; set; }
        public String Direction { get; set; }
        public short? Localite { get; set; }
        public String Sexe{ get; set; }
        public string TelPersonnel { get; set; }
        public DateTime? AncGrade { get; set; }
        public DateTime? AncEchelon { get; set; }
        public string CINA { get; set; }
        public int? CINN { get; set; }
        public String Email { get; set; }
        public String CodeGrade { get; set; }
        public String CodeCateg { get; set; }
        public String CodeCorps { get; set; }
        public String CodeCadre { get; set; }
    }
}

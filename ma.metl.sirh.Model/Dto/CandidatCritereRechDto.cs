using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ma.metl.sirh.Model.Dto
{
    public class CandidatCritereRechDto
    {
        public string AnneeProm { get; set; }
        public int GradeId { get; set; }
        public int ExamenId { get; set; }
        public int RegionId { get; set; }
        public int CommissionId { get; set; }
        public int DirectionId { get; set; }
        public int SpecialiteId { get; set; }
        public int AncienneteGrade { get; set; }
        public int AncienneteEchelon { get; set; }
        public DateTime? DateRecrutement { get; set; }
        public string NumDoti { get; set; }
        public DateTime? DateNaissance { get; set; }
        public EtatCandidature EtatCandidature { get; set; }
        public Resultats Resultat { get; set; }
        public EtatPromotion EtatProm { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ma.metl.sirh.Model.Dto
{
    public class CandidatResultatDto
    {
        public long Id { get; set; }
        public long candidatureId { get; set; }
        public String NumDoti { get; set; }
        public String Nom { get; set; }
        public String NomA { get; set; }
        public String Prenom { get; set; }
        public String PrenomA { get; set; }
        public String Email { get; set; }
        public String AncienGrade { get; set; }
        public String AncienGradeA { get; set; }
        public String AncienGradeCadreCode { get; set; }
        public String AncienGradeCode { get; set; }
        public String NouveauGrade { get; set; }
        public String NouveauGradeA { get; set; }
        public String NouveauGradeCadreCode { get; set; }
        public String NouveauGradeCode { get; set; }
        public String Direction { get; set; }
        public String DirectionA { get; set; }
        public String Localite { get; set; }
        public String Statut { get; set; }
        public string TelPersonnel { get; set; }
        public String DecisionCap { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DateEffet { get; set; }
        public string Annee { get; set; }
        public DateTime AncienneteGrade { get; set; }
        public DateTime AncienneteEchelon { get; set; }
        public int GradeId { get; set; }
        public int DirectionId { get; set; }
        public int SpecialiteId { get; set; }
        public int LocaliteId { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DateRecrutement { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DateNaissance { get; set; }
        public string Sexe { get; set; }
        public string CIN { get; set; }
        public List<DetailAvancement> AvancementsList { get; set; }
        public List<Sanction> Sanctions { get; set; }
        public List<Historique> Historiques { get; set; }
        public decimal NoteMoyenne { get; set; }
        public long detailAvancement { get; set; }
        public bool Selected { get; set; }
        public int ordreMerite { get; set; }

        // champs notation 
        public decimal NoteEcrite { get; set; }
        public decimal NotePresConnaissanceG { get; set; }
        public decimal NoteConnaissMin { get; set; }
        public decimal NoteConnaissSp { get; set; }
        public decimal NoteOrale { get; set; }
        public decimal NoteGlobale { get; set; }
       
    }
}

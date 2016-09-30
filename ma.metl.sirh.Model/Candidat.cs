using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace ma.metl.sirh.Model
{
    [Table("Candidat")]
    public class Candidat : AuditableEntity<long>
    {
        public Candidat()
        {
            AvancementsList = new List<DetailAvancement>();
            Sanctions = new List<Sanction>();
            Historiques = new List<Historique>();
            candidatures = new List<Candidature>();
        }

        public string NumDoti { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomAR { get; set; }
        public string PrenomAR { get; set; }
        public string Email { get; set; }
        public string TelPersonnel { get; set; }
        public string Sexe { get; set; }
        public string CIN { get; set; }

        public DateTime? DateNaissance { get; set; }

        public DateTime? DateRecrutement { get; set; }

        public DateTime? DateEffet { get; set; }

        public long DirectionId { get; set; }
        [ForeignKey("DirectionId")]
        public virtual Direction Direction { get; set; }

        public long GradeId { get; set; }
        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        public long LocaliteId { get; set; }
        [ForeignKey("LocaliteId")]
        public virtual Localite Localite { get; set; }

        public virtual List<DetailAvancement> AvancementsList { get; set; }
        public virtual List<Sanction> Sanctions { get; set; }
        public virtual List<Historique> Historiques { get; set; }
        public virtual List<Candidature> candidatures { get; set; }
   }
}

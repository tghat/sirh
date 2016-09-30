using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ma.metl.sirh.Model
{
    public class ProgrammeModel
    {
        public string IdFlux { get; set; }

        public Etat Etat { get; set; }

        public TypeFlux TypeFlux { get; set; }

        public string Annee { get; set; }

        public ReunionStatus ReunionStatus { get; set; }

        public EtatPublication EtatPublication { get; set; }

        public TypePublication TypePublication { get; set; }

        public TypePublicationAVC TypePublicationAVC { get; set; }

        public DateTime? DateFin { get; set; }

        public DateTime? DateDebut { get; set; }

    }

}
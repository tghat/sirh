//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace codefirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class Commission
    {
        public long Id { get; set; }
        public string Titre { get; set; }
        public string Annee { get; set; }
        public string EcritOrOral { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public long GradeId { get; set; }
    }
}

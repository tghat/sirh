//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ma.metl.sirh.Model.Ora
{
    using System;
    using System.Collections.Generic;

    public partial class SIT_ADM : AuditableEntity<long>
    {
        public int COD_AG { get; set; }
        public string COD_CATEG { get; set; }
        public string COD_CORPS { get; set; }
        public string COD_CADRE { get; set; }
        public string COD_GRADE { get; set; }
        public Nullable<short> COD_ELO { get; set; }
        public Nullable<short> COD_FCT { get; set; }
        public string COD_S_ST { get; set; }
        public Nullable<short> COD_ORG_DET { get; set; }
        public Nullable<short> MOD_AV_GRADE { get; set; }
        public Nullable<short> COD_ECH { get; set; }
        public Nullable<System.DateTime> DAT_FIN_C { get; set; }
        public Nullable<short> TAUX_PRIME { get; set; }
        public Nullable<short> SPECIALITE { get; set; }
        public Nullable<System.DateTime> DAT_EFF_GR { get; set; }
        public Nullable<System.DateTime> ANC_GRADE { get; set; }
        public Nullable<System.DateTime> DAT_EFF_ELO { get; set; }
        public Nullable<System.DateTime> ANC_ELO { get; set; }
        public Nullable<System.DateTime> DAT_FCT { get; set; }
        public Nullable<System.DateTime> DAT_S_ST { get; set; }
        public string COD_CATEG_ASS { get; set; }
        public string COD_CORPS_ASS { get; set; }
        public string COD_CADRE_ASS { get; set; }
        public string COD_GRADE_ASS { get; set; }
        public Nullable<System.DateTime> ANC_CADRE { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ma.metl.sirh.Model.Ora
{
    using System;
    using System.Collections.Generic;
    
    public partial class ACTE_HIST
    {
        public int REF_ARRETE { get; set; }
        public short ANN_VISA { get; set; }
        public Nullable<int> NUM_BORD { get; set; }
        public Nullable<short> COD_USR { get; set; }
        public string DECISION { get; set; }
        public Nullable<System.DateTime> DAT_DEC { get; set; }
        public Nullable<System.DateTime> DAT_CHARG { get; set; }
        public string STADE { get; set; }
        public string OBSERV { get; set; }
        public string B_CHECK { get; set; }
        public string CODE_SOUS_ORD { get; set; }
    }
}
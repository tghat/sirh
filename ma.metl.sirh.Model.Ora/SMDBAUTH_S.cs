//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ma.metl.sirh.Model.Ora
{
    using System;
    using System.Collections.Generic;
    
    public partial class SMDBAUTH_S
    {
        public int NAMEDOBJECT_ID_SEQUENCEID_ { get; set; }
        public Nullable<int> NAMEDOBJECT_ID_OBJECTTYPE_ { get; set; }
        public string DBAUTH_CONSOLEUSER_ { get; set; }
        public string DBAUTH_DBUSERNAME_ { get; set; }
        public Nullable<int> DBAUTH_ISDBUSERNAMESET_ { get; set; }
        public string DBAUTH_DBPASSWORD_ { get; set; }
        public Nullable<int> DBAUTH_ISDBPASSWORDSET_ { get; set; }
        public string DBAUTH_INTERNALPASSWORD_ { get; set; }
        public Nullable<int> DBAUTH_ISINTERNALPASSWORDSET_ { get; set; }
    }
}

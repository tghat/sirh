using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ma.metl.sirh.Model.Dto
{
    public class CritereRapportViewModel
    {
        public short? UserCode { get; set; }
        public int? GradeId { get; set; }
        public string OperationId { get; set; }
        public int? RefArrete { get; set; }
        public DateTime? DateOperation { get; set; }
    }
}
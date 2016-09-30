using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Grade")]
    public class Grade : AuditableEntity<long>
    {
        public string Description { get; set; }

        public string DescriptionAM { get; set; }

        public string DescriptionAF { get; set; }

        public string CodeCateg { get; set; }

        public string CodeCadre { get; set; }

        public string CodeCorps { get; set; }

        public string CodeGrade { get; set; }
    }
}

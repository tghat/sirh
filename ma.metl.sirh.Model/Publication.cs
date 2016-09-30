using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    [Table("Publication")]
    public class Publication : AuditableEntity<long>
    {
        public string TypePublication { get; set; }
        public string Objet { get; set; }
        public DateTime DateDebutPub { get; set; }
        public DateTime DateFinPub { get; set; }
        public byte[] PieceJointePub { get; set; }
        public EtatPublication Statut { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        // type : ave ou avc 
        public string Type { get; set; }
    }
}

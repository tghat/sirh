using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model
{
    public class Email
    {
        public long DirectionId { get; set; }
        public string Objet { get; set; }
        public string Destinataire { get; set; }
        public string Message { get; set; }
    }
}

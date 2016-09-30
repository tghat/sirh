using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FluentValidation.Mvc;
using FluentValidation;

namespace ma.metl.sirh.Model
{
    [Table("Direction")]
    [FluentValidation.Attributes.Validator(typeof(DirectionValidator))]
    public class Direction : AuditableEntity<long>
    {

        
        public string Code { get; set; }

        public string Description { get; set; }

        public string DescriptionA { get; set; }


        
    }

    public class DirectionValidator : AbstractValidator<Direction>
    {
        public DirectionValidator()
        {
          //  RuleFor(x => x.Id).NotNull();
           

        }
    }
}

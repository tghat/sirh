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
    [Table("Service")]
    [FluentValidation.Attributes.Validator(typeof(ServiceValidator))]
    public class Service : AuditableEntity<long>
    {

       
        public string Code { get; set; }

        public string Description { get; set; }


        
    }

    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator()
        {
        //   RuleFor(x => x.Id).NotNull();
           

        }
    }
}

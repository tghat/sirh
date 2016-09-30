using FluentValidation;
using ma.metl.sirh.Model.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Model.Dto
{
    [FluentValidation.Attributes.Validator(typeof(EventDtoValidator))]
    public class EventDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string StartDateString { get; set; }

        public string EndDateString { get; set; }

        [RegularExpression(@"\d{2}:\d{2}", ErrorMessageResourceName = "Invalid", ErrorMessageResourceType = typeof(Resources))]
        public string Duree { get; set; }

        public string DureeString { get; set; }

        public string StatusString { get; set; }

        public string StatusColor { get; set; }

        public string ClassName { get; set; }

        [RegularExpression(@"\d{2}:\d{2}", ErrorMessageResourceName = "Invalid", ErrorMessageResourceType = typeof(Resources))]
        public string Heure { get; set; }

        public int CommissionId { get; set; }

        public class EventDtoValidator : AbstractValidator<EventDto>
        {
            public EventDtoValidator()
            {
                


            }

        }
    }
}

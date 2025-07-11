using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Miscellaneous
{
    public class CustomValidation
    {
        public class NotPastDateAttribute : ValidationAttribute
        {
            public NotPastDateAttribute()
            {
                ErrorMessage = "Date cannot be in the past";
            }

            public override bool IsValid(object value)
            {
                if (value is DateTime date)
                {
                    return date.Date >= DateTime.Today;
                }
                return false;
            }
        }

        public class EndDateAfterStartDateAttribute : ValidationAttribute
        {
            private readonly string _startDatePropertyName;

            public EndDateAfterStartDateAttribute(string startDatePropertyName)
            {
                _startDatePropertyName = startDatePropertyName;
                ErrorMessage = "End date must be after start date";
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);
                if (startDateProperty == null)
                {
                    return new ValidationResult($"Unknown property: {_startDatePropertyName}");
                }

                var startDate = (DateTime?)startDateProperty.GetValue(validationContext.ObjectInstance);
                var endDate = (DateTime)value;

                if (endDate <= startDate)
                {
                    return new ValidationResult(ErrorMessage ?? "End date must be after start date");
                }

                return ValidationResult.Success;
            }
        }
    }
}

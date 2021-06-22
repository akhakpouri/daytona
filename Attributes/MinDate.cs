using System;
using System.ComponentModel.DataAnnotations;
using Daytona.Helpers;

namespace Daytona.Attributes
{
    public class MinDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is DateTime))
                return ValidationResult.Success;
            

            if ((DateTime)value >= DateTimeHelper.MinimumDate)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Please enter a valid date");
        }
    }
}

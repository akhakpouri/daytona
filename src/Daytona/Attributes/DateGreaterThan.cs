using System;
using System.ComponentModel.DataAnnotations;

namespace Daytona.Attributes
{
    public class DateGreaterThan : ValidationAttribute
    {
        readonly string _comparisonProperty;

        public DateGreaterThan(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            if (value == null || !(value is DateTime currentValue))
                return ValidationResult.Success;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);
            if (comparisonValue.HasValue && currentValue < comparisonValue.Value)
                return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
        }
    }
}

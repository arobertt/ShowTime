using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _otherPropertyName;

    public DateGreaterThanAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);

        if (otherProperty == null)
            return new ValidationResult($"Unknown property: {_otherPropertyName}");

        var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

        if (value == null || otherValue == null)
            return ValidationResult.Success;        

        if (value is DateTime thisDate && otherValue is DateTime otherDate)
        {
            if (thisDate < otherDate)
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be after {_otherPropertyName}.");
            }
        }

        return ValidationResult.Success;
    }
}

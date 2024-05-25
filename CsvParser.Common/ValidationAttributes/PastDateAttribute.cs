using System.ComponentModel.DataAnnotations;


namespace CsvParser.Common.ValidationAttributes
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value is DateTime dateValue)
            {
                if (dateValue >= DateTime.Now)
                {
                    return new ValidationResult("The date must be in the past.");
                }
            }
            return ValidationResult.Success;
        }
    }

}

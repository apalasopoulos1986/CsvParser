using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace CsvParser.Common.ValidationAttributes
{
    public class DollarCurrencyAmountAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string pattern = @"\$\d+\.\d{1,2}";
                if (!Regex.IsMatch(value.ToString(), pattern))
                {
                    return new ValidationResult("Invalid amount format. Must include currency symbol and amount.");
                }
            }
            return ValidationResult.Success;
        }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace CsvParser.Common.ValidationAttributes
{
    public class CurrencyAmountAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string pattern = @"^[A-Z]{1}[0-9,]+(\.[0-9]{1,2})?$";
                if (!Regex.IsMatch(value.ToString(), pattern))
                {
                    return new ValidationResult("Invalid amount format. Must include currency symbol and amount.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
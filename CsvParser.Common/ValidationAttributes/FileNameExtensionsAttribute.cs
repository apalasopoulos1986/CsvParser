using System.ComponentModel.DataAnnotations;


namespace CsvParser.Common.ValidationAttributes
{
    public class FileNameExtensionsAttribute : ValidationAttribute
    {
        public string[] ValidExtensions { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string extension = Path.GetExtension(value.ToString()).TrimStart('.').ToLower();
                if (!ValidExtensions.Contains(extension))
                {
                    return new ValidationResult($"Invalid file extension. Allowed extensions are: {string.Join(", ", ValidExtensions)}");
                }
            }
            return ValidationResult.Success;
        }
    }

}

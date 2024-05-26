using CsvParser.Common.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace CsvParser.Common.Requests
{
    public class ApplicationTransactionCreationRequest
    {
        [Required]
        [MaxLength(200)]
        public string ApplicationName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(300)]
        [FileNameExtensions(ValidExtensions = new string[] { "png", "mp3", "tiff", "xls", "pdf" })]
        public string Filename { get; set; }

        [Url]
        public string Url { get; set; }

        [Required]
        [PastDate]
        public DateTime Inception { get; set; }

        [Required]
        [DollarCurrencyAmount]
        public string Amount { get; set; }

        [Range(0, 100)]
        public decimal? Allocation { get; set; }
    }
}

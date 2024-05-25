using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CsvParser.Db.DbEntities;
using Microsoft.AspNetCore.Http;
using CsvParser.Service.Interfaces;
using CsvParser.Service.Mapping;

namespace CsvParser.Service.Services
{
    public class CsvTransactionParsingService:ICsvTransactionParsingService
    {
        public async Task<List<ApplicationTransaction>> ParseCsvAsync(IFormFile file)
        {
            var transactions = new List<ApplicationTransaction>();

            using (var stream = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<ApplicationTransactionMap>();
                var records = csv.GetRecords<ApplicationTransaction>().ToList();

                foreach (var record in records)
                {
                    var errors = ValidateTransaction(record);
                    if (errors.Count == 0)
                    {
                        record.Id = Guid.NewGuid();
                        transactions.Add(record);
                    }
                    else
                    {
                       
                        Console.WriteLine(string.Join(", ", errors));
                    }
                }
            }

            return transactions;
        }

        private List<string> ValidateTransaction(ApplicationTransaction transaction)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(transaction.ApplicationName) || transaction.ApplicationName.Length > 200)
                errors.Add("Invalid ApplicationName");

            if (string.IsNullOrWhiteSpace(transaction.Email) || transaction.Email.Length > 200)
                errors.Add("Invalid Email");

            if (!string.IsNullOrWhiteSpace(transaction.Filename) && transaction.Filename.Length > 300)
                errors.Add("Invalid Filename");

            var validExtensions = new[] { ".png", ".mp3", ".tiff", ".xls", ".pdf" };
            if (!string.IsNullOrWhiteSpace(transaction.Filename) && !validExtensions.Contains(Path.GetExtension(transaction.Filename)))
                errors.Add("Invalid Filename extension");

            if (!string.IsNullOrWhiteSpace(transaction.Url) && !Uri.IsWellFormedUriString(transaction.Url, UriKind.Absolute))
                errors.Add("Invalid Url");

            if (transaction.Inception >= DateTime.Now)
                errors.Add("Inception date must be in the past");

            if (transaction.Allocation.HasValue && (transaction.Allocation < 0 || transaction.Allocation > 100))
                errors.Add("Allocation must be between 0 and 100");

  

            return errors;
        }

    }
}

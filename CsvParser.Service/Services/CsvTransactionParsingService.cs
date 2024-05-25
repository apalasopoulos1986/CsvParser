using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CsvParser.Db.DbEntities;
using Microsoft.AspNetCore.Http;
using CsvParser.Service.Interfaces;
using CsvParser.Service.Mapping;
using CsvParser.Db.Interfaces;

namespace CsvParser.Service.Services
{
    public class CsvTransactionParsingService:ICsvTransactionParsingService
    {

        private readonly ITransactionsRepository _transactionsRepository;

        public CsvTransactionParsingService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }
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
                    var isUpdate=_transactionsRepository.IsUpdate(record.Id);

                    var errors = ValidateTransaction(record, isUpdate);
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

        private List<string> ValidateTransaction(ApplicationTransaction transaction, bool isUpdate = false)
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


            if (string.IsNullOrWhiteSpace(transaction.Amount) || !IsValidAmount(transaction.Amount))
                errors.Add("Invalid Amount");

            if (isUpdate && !IsSameCurrency(transaction))
                errors.Add("Currency cannot be changed for an existing transaction");
            return errors;
        }

        private bool IsSameCurrency(ApplicationTransaction transaction)
        {
            var isSameCurrency = _transactionsRepository.IsSameCurrency(transaction);

            return isSameCurrency;
        }

        private bool IsValidAmount(string amount)
        {
            // Example pattern to validate amounts with dollar sign: "$123.45"
            var pattern = @"^\$\d+(\.\d{1,2})?$";
            return System.Text.RegularExpressions.Regex.IsMatch(amount, pattern);
        }

       

    }
}

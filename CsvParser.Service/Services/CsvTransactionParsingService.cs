using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CsvParser.Db.DbEntities;
using Microsoft.AspNetCore.Http;
using CsvParser.Service.Interfaces;
using CsvParser.Service.Mapping;
using CsvParser.Db.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CsvParser.Service.Services
{
    public class CsvTransactionParsingService : ICsvTransactionParsingService
    {

        private readonly ITransactionsRepository _transactionsRepository;

        public CsvTransactionParsingService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }
        public async Task<bool> UploadCsvAsync(IFormFile file)
        {
            var errors = new List<string>();
            int totalRows = 0;

            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.Context.RegisterClassMap<ApplicationTransactionMap>();

                    var records = csv.GetRecords<ApplicationTransaction>().ToList();

                    totalRows = records.Count;
                                      
                    for (int index = 0; index < records.Count; index++)
                    {
                        var record = records[index];

                        if (record.Id == Guid.Empty)
                        {
                            errors.Add($"Row {index + 1}: Record with empty Id found. Skipping this record.");
                            continue;
                        }

                        var validationResults = new List<ValidationResult>();
                        var validationContext = new ValidationContext(record);

                        if (!Validator.TryValidateObject(record, validationContext, validationResults, true))
                        {
                            errors.AddRange(validationResults.Select(vr => $"Row {index + 1}: {vr.ErrorMessage}"));
                            continue;
                        }

                        var isUpdate = await _transactionsRepository.IsUpdate(record.Id);
                        if (isUpdate)
                        {
                            var existingTransaction = await _transactionsRepository.GetTransaction(record.Id);
                            if (!IsSameCurrency(existingTransaction.Amount, record.Amount))
                            {
                                errors.Add($"Row {index + 1}: Currency change is not allowed for transaction ID: {record.Id}");
                                continue;
                            }
                        }
                       
                        await _transactionsRepository.UpsertTransaction(record);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                // _logger.LogError(ex, "Error processing CSV file.");
                return false;
            }
        }
        private bool IsSameCurrency(string existingAmount, string newAmount)
        {
            var existingCurrencySymbol = existingAmount[0];
            var newCurrencySymbol = newAmount[0];
            return existingCurrencySymbol == newCurrencySymbol;
        }

    }
}

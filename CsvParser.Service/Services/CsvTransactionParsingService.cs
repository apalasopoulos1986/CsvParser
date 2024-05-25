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
        public async Task<bool> UploadCsvAsync(IFormFile file)
        {

            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.Context.RegisterClassMap<ApplicationTransactionMap>();

                    var records = csv.GetRecords<ApplicationTransaction>().ToList();

                    foreach (var record in records)
                    {
                        if (record.Id == Guid.Empty)
                        {
                            record.Id = Guid.NewGuid(); 
                        }
                        _transactionsRepository.UpsertTransaction(record);
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

      
       

    }
}

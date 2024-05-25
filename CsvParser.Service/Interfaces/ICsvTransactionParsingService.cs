using CsvParser.Common.Models;
using CsvParser.Db.DbEntities;
using Microsoft.AspNetCore.Http;


namespace CsvParser.Service.Interfaces
{
    public interface ICsvTransactionParsingService
    {
        public Task<bool> UploadCsvAsync(IFormFile file);

        public Task<PaginatedResult<ApplicationTransaction>> GetPaginatedTransactionsAsync(int page, int pageSize);

        public Task<bool> DeleteTransactionAsync(Guid id);


        public Task<ApplicationTransaction> GetTransactionAsync(Guid id);
    }
}

using CsvParser.Common.Requests;
using CsvParser.Common.Responses;
using CsvParser.Db.DbEntities;
using Microsoft.AspNetCore.Http;


namespace CsvParser.Service.Interfaces
{
    public interface ICsvTransactionParsingService
    {
        public Task<bool> UploadCsvAsync(IFormFile file);

        public Task<GenericResponse> UpsertTransactionAsync(ApplicationTransactionUpsertRequest request);

        public Task<PaginatedResponse<ApplicationTransaction>> GetPaginatedTransactionsAsync(int page, int pageSize);

        public Task<bool> DeleteTransactionAsync(Guid id);

        public Task<ApplicationTransaction> GetTransactionAsync(Guid id);
    }
}

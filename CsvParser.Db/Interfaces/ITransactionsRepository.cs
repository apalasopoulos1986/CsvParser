using CsvParser.Db.DbEntities;


namespace CsvParser.Db.Interfaces
{
    public interface ITransactionsRepository
    {
        public Task UpsertTransaction(ApplicationTransaction transaction);
        public Task<IEnumerable<ApplicationTransaction>> GetTransactions(int page, int pageSize);

        public Task<ApplicationTransaction> GetTransaction(Guid id);

        public Task DeleteTransaction(Guid id);

        public Task<bool> IsSameCurrency(ApplicationTransaction transaction);

        public Task<bool> IsUpdate(Guid id);

        public  Task<int> GetTotalTransactionCount();
    }
}

using CsvParser.Db.DbEntities;


namespace CsvParser.Db.Interfaces
{
    public interface ITransactionsRepository
    {
        public void UpsertTransaction(ApplicationTransaction transaction);
        public IEnumerable<ApplicationTransaction> GetTransactions(int page, int pageSize);

        public ApplicationTransaction GetTransaction(Guid id);

        public void DeleteTransaction(Guid id);

        public bool IsSameCurrency(ApplicationTransaction transaction);

        public bool IsUpdate(Guid id);
    }
}

using CsvParser.Db.Context;
using CsvParser.Db.DbEntities;
using CsvParser.Db.Interfaces;
using Dapper;




namespace CsvParser.Db.Repository
{
    public class TransactionsRepository:ITransactionsRepository
    {
        private readonly DapperContext _context;

        public TransactionsRepository(DapperContext context)=>_context = context;

        private static string UpsertTransactionQuery = @" MERGE ApplicationTransactions AS target 
                                                           USING (SELECT @Id AS Id) AS source
                                                           ON (target.Id = source.Id)
                                                           WHEN MATCHED THEN 
                                                           UPDATE SET 
                                                           ApplicationName = @ApplicationName,
                                                           Email = @Email, 
                                                           Filename = @Filename,
                                                           Url = @Url,
                                                           Inception = @Inception, 
                                                           Amount = @Amount,
                                                           Allocation = @Allocation
                                                           WHEN NOT MATCHED THEN
                                                           INSERT (Id, ApplicationName, Email, Filename, Url, Inception, Amount, Allocation)
                                                           VALUES (@Id, @ApplicationName, @Email, @Filename, @Url, @Inception, @Amount, @Allocation); 
                                                          ";


        private static string GetTransactionsQuery = @"
                                                     SELECT * FROM ApplicationTransactions
                                                     ORDER BY Inception
                                                     OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; ";

        private static string GetTransactionQuery = @" SELECT * FROM ApplicationTransactions WHERE Id = @Id ";


        private static string DeleteTransactionQuery = @" DELETE FROM ApplicationTransactions WHERE Id = @Id ";


        private static string ExistingCurrencyAmountQuery = @" SELECT Amount FROM ApplicationTransactions WHERE Id = @Id ";


        private static string IsUpdateQuery = @" SELECT COUNT(1) FROM ApplicationTransactions WHERE Id = @Id ";

        private static string GetTotalTransactionCountQuery = @"SELECT COUNT(*) FROM ApplicationTransactions;";

        public async Task UpsertTransaction(ApplicationTransaction transaction)
        {
            using (var connection = _context.CreateConnection())
            {               
               await connection.ExecuteAsync(UpsertTransactionQuery, transaction);
            }
        }

        public async Task<IEnumerable<ApplicationTransaction>> GetTransactions(int page, int pageSize)
        {
            using (var connection = _context.CreateConnection())
            {

                return await connection.QueryAsync<ApplicationTransaction>(GetTransactionsQuery, new { PageSize = pageSize, Offset = (page - 1) * pageSize });
            }
        }

        public async Task<ApplicationTransaction> GetTransaction(Guid id)
        {
            using (var connection = _context.CreateConnection())
            {
               
                return await connection.QuerySingleOrDefaultAsync<ApplicationTransaction>(GetTransactionQuery, new { Id = id });
            }
        }

        public async Task  DeleteTransaction(Guid id)
        {
            using (var connection = _context.CreateConnection())
            {               
               await connection.ExecuteAsync(DeleteTransactionQuery, new { Id = id });
            }
        }



        public async Task<bool>  IsUpdate(Guid id)
        {
            using (var connection = _context.CreateConnection())
            {
                var isUpdate= await  connection.QuerySingleOrDefaultAsync<int>(IsUpdateQuery, new { Id=id }) > 0;

                return isUpdate;
            }
        }


        public async Task<int> GetTotalTransactionCount()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<int>(GetTotalTransactionCountQuery);
            }
        }
        public async Task<bool> IsSameCurrencyFromDb(ApplicationTransaction transaction)
        {
            using (var connection = _context.CreateConnection())
            {
                var existingCurrencyAmount = await connection.QuerySingleOrDefaultAsync<ApplicationTransaction>(ExistingCurrencyAmountQuery, new { transaction.Id });

                if (existingCurrencyAmount == null) return true;

                var existingCurrencySymbol = existingCurrencyAmount.Amount[0];
                var newCurrencySymbol = transaction.Amount[0];

                return existingCurrencySymbol == newCurrencySymbol;
            }
        }

    }
}

using CsvParser.Db.Context;
using CsvParser.Db.DbEntities;
using CsvParser.Db.Interfaces;
using Dapper;
using System.Data.SqlClient;


namespace CsvParser.Db.Repository
{
    public class TransactionsRepository:ITransactionsRepository
    {
        private readonly DapperContext _context;

        public TransactionsRepository(DapperContext context)=>_context = context;

        private static string UpsertTransactionsQuery = @" MERGE ApplicationTransactions AS target 
                                                          USING (SELECT @Id AS Id) AS source
                                                          ON (target.Id = source.Id) WHEN MATCHED THEN 
                                                          UPDATE SET ApplicationName = @ApplicationName,
                                                          Email = @Email, 
                                                          Filename = @Filename,
                                                          Url = @Url,
                                                          Inception =@Inception, 
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


        public void UpsertTransaction(ApplicationTransaction transaction)
        {
            using (var connection = _context.CreateConnection())
            {               
                connection.Execute(UpsertTransactionsQuery, transaction);
            }
        }

        public IEnumerable<ApplicationTransaction> GetTransactions(int page, int pageSize)
        {
            using (var connection = _context.CreateConnection())
            {

                return connection.Query<ApplicationTransaction>(GetTransactionsQuery, new { PageSize = pageSize, Offset = (page - 1) * pageSize });
            }
        }

        public ApplicationTransaction GetTransaction(Guid id)
        {
            using (var connection = _context.CreateConnection())
            {
               
                return connection.QuerySingleOrDefault<ApplicationTransaction>(GetTransactionQuery, new { Id = id });
            }
        }

        public void DeleteTransaction(Guid id)
        {
            using (var connection = _context.CreateConnection())
            {               
                connection.Execute(DeleteTransactionQuery, new { Id = id });
            }
        }


    }
}

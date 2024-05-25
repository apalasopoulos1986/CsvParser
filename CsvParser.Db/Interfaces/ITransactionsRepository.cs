using CsvParser.Db.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvParser.Db.Interfaces
{
    public interface ITransactionsRepository
    {
        public void UpsertTransaction(ApplicationTransaction transaction);
        public IEnumerable<ApplicationTransaction> GetTransactions(int page, int pageSize);

        public ApplicationTransaction GetTransaction(Guid id);

        public void DeleteTransaction(Guid id);
    }
}

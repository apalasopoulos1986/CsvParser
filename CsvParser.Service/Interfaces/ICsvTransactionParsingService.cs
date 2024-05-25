using CsvParser.Db.DbEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvParser.Service.Interfaces
{
    public interface ICsvTransactionParsingService
    {
        public Task<List<ApplicationTransaction>> ParseCsvAsync(IFormFile file);
    }
}

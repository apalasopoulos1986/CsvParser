using CsvHelper.Configuration;
using CsvParser.Db.DbEntities;
using CsvParser.Service.HelperMethods;

namespace CsvParser.Service.Mapping
{
    public sealed class ApplicationTransactionMap : ClassMap<ApplicationTransaction>
    {
        public ApplicationTransactionMap()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.ApplicationName);
            Map(m => m.Email);
            Map(m => m.Filename);
            Map(m => m.Url);
            Map(m => m.Inception);
            Map(m => m.Amount);
            Map(m => m.Allocation);
        }
    }
}



namespace CsvParser.Db.DbEntities
{
    public class ApplicationTransaction
    {
        public Guid Id { get; set; } 
        public string ApplicationName { get; set; }
        public string Email { get; set; }
        public string Filename { get; set; }
        public string Url { get; set; }
        public DateTime Inception { get; set; }
        public decimal Amount { get; set; }
        public decimal? Allocation { get; set; }
    }
}

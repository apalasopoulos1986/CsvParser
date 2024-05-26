using CsvParser.Service.Interfaces;
using CsvParser.Service.Services;

namespace CsvParser.Api.Configuration
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            {
                services.AddTransient<ICsvTransactionParsingService, CsvTransactionParsingService>();
            }
           

            return services;
        }

    }
}


using CsvParser.Common.ConfigSettings;
using CsvParser.Common.Logging;

namespace CsvParser.Api.Configuration
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddLoggingFile(this ILoggingBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<FileLoggerOptions>(configuration);
            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            return builder;
        }
    }
}

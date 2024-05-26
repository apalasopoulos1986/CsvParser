using CsvParser.Common;

namespace CsvParser.Api.Configuration
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<FileLoggerOptions>(configuration);
            builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            return builder;
        }
    }
}

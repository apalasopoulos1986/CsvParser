using CsvParser.Common.ConfigSettings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace CsvParser.Common.Logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _filePath;

        public FileLoggerProvider(IOptions<FileLoggerOptions> options)
        {
            _filePath = options.Value.Path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath);
        }

        public void Dispose() { }
    }

   
}

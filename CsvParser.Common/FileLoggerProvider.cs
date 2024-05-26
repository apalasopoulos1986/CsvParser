using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace CsvParser.Common
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

    public class FileLoggerOptions
    {
        public string Path { get; set; }
    }
}

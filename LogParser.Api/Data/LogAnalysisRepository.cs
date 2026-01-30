using LogParser.Api.Models;
using LogParser.Api.Services;

namespace LogParser.Api.Data
{
    public class LogAnalysisRepository : ILogAnalysisRepository
    {
        private readonly ILogParserService parser;

        public LogAnalysisRepository(ILogParserService parser)
        {
            this.parser = parser;
        }

        public async Task<LogAnalysisResult> AnalyzeAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file.Length == 0)
                return new LogAnalysisResult();

            var lines = new List<string>();

            using var reader = new StreamReader(file.OpenReadStream());
            while (!reader.EndOfStream)
            {
                cancellationToken.ThrowIfCancellationRequested();
                lines.Add(await reader.ReadLineAsync() ?? string.Empty);
            }

            return await parser.Analyze(lines, cancellationToken);
        }
    }
}

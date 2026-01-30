using LogParser.Api.Models;

namespace LogParser.Api.Services
{
    public interface ILogParserService
    {
        Task<LogAnalysisResult> Analyze(IEnumerable<string> logLines, CancellationToken cancellationToken = default);
    }
}

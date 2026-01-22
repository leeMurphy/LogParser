using LogParser.Api.Models;

namespace LogParser.Api.Services
{
    public interface ILogParserService
    {
        LogAnalysisResult Analyze(IEnumerable<string> logLines);
    }
}

using LogParser.Api.Models;

namespace LogParser.Api.Data
{
    public interface ILogAnalysisRepository
    {
        Task<LogAnalysisResult> AnalyzeAsync(IFormFile file);
    }
}

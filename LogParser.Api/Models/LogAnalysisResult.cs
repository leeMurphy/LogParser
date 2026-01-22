namespace LogParser.Api.Models
{
    public sealed class LogAnalysisResult
    {
        public int UniqueIpCount { get; init; }
        public IReadOnlyList<string> TopUrls { get; init; } = [];
        public IReadOnlyList<string> TopIpAddresses { get; init; } = [];
    }
}

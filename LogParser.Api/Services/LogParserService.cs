using LogParser.Api.Models;
using System.Text.RegularExpressions;

namespace LogParser.Api.Services
{
    public sealed class LogParserService : ILogParserService
    {
        private static readonly Regex LogRegex = new(@"^(?<ip>\S+).+?""\w+\s+(?<url>\S+)",  RegexOptions.Compiled);

        public Task<LogAnalysisResult> Analyze(IEnumerable<string> logLines, CancellationToken cancellationToken = default)
        {
            var ipCounts = new Dictionary<string, int>();
            var urlCounts = new Dictionary<string, int>();

            foreach (var line in logLines)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var match = LogRegex.Match(line);
                if (!match.Success)
                    continue;

                var ip = match.Groups["ip"].Value;
                var url = match.Groups["url"].Value;

                cancellationToken.ThrowIfCancellationRequested();

                ipCounts[ip] = ipCounts.GetValueOrDefault(ip) + 1;
                urlCounts[url] = urlCounts.GetValueOrDefault(url) + 1;
            }

            var result = new LogAnalysisResult
            {
                UniqueIpCount = ipCounts.Count,
                TopUrls = urlCounts
                    .OrderByDescending(x => x.Value)
                    .Take(3)
                    .Select(x => x.Key)
                    .ToList(),
                TopIpAddresses = ipCounts
                    .OrderByDescending(x => x.Value)
                    .Take(3)
                    .Select(x => x.Key)
                    .ToList()
            };

            return Task.FromResult(result);
        }
    }
}

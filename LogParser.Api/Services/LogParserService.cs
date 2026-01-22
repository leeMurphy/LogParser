using LogParser.Api.Models;
using System.Text.RegularExpressions;

namespace LogParser.Api.Services
{
    public sealed class LogParserService : ILogParserService
    {
        private static readonly Regex LogRegex =
            new(@"^(?<ip>\S+).+?""\w+\s+(?<url>\S+)",  RegexOptions.Compiled);

        public LogAnalysisResult Analyze(IEnumerable<string> logLines)
        {
            var ipCounts = new Dictionary<string, int>();
            var urlCounts = new Dictionary<string, int>();

            foreach (var line in logLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var match = LogRegex.Match(line);
                if (!match.Success)
                    continue;

                var ip = match.Groups["ip"].Value;
                var url = match.Groups["url"].Value;

                ipCounts[ip] = ipCounts.GetValueOrDefault(ip) + 1;
                urlCounts[url] = urlCounts.GetValueOrDefault(url) + 1;
            }

            return new LogAnalysisResult
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
        }
    }
}

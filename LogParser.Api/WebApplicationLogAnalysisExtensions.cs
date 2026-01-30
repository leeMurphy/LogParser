using LogParser.Api.Data;
using LogParser.Api.Models;

namespace LogParser.Api
{
    public static class WebApplicationLogAnalysisExtensions
    {
        public static void MapLogAnalysisEndpoints(this WebApplication app)
        {
            app.MapPost("/analyze", async (IFormFile file, ILogAnalysisRepository repo, CancellationToken cancellationToken) =>
            {
                if (file is null)
                    return Results.BadRequest("File is required");
                if (file.Length == 0)
                    return Results.BadRequest("File is empty");

                var result = await repo.AnalyzeAsync(file, cancellationToken);
                return Results.Ok(result);
            })
            .Produces<LogAnalysisResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .DisableAntiforgery();
        }
    }
}

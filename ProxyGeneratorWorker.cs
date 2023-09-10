using System.Diagnostics;
using System.Text.Json;

namespace Dotnet.Web.Nuxt;

public static class ProxyGenerator
{
    [Conditional("DEBUG")]
    public static void GenerateSpaProxyRoutes(this WebApplication app)
    {
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            var sources = app.Services.GetRequiredService<IEnumerable<EndpointDataSource>>();

            var routes = sources
                .SelectMany(s => s.Endpoints)
                .Where(e => e is RouteEndpoint)
                .Cast<RouteEndpoint>();

            var paths = routes
                .Select(r => r.RoutePattern.RawText)
                .Where(t => !string.IsNullOrEmpty(t) && !t.Contains("nonfile"))
                .Distinct();

            var filename = Path.Combine(app.Environment.ContentRootPath, "ClientApp/proxy-paths.json");
            var json = JsonSerializer.Serialize(
                paths, new JsonSerializerOptions(JsonSerializerOptions.Default) { WriteIndented = true }
            );

            File.WriteAllText(filename, json);
        });
    }
}
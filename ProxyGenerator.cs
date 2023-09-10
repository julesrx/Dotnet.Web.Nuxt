using System.Diagnostics;
using System.Text.Json;

namespace Spartacus;

public static class ProxyGenerator
{
    [Conditional("DEBUG")]
    public static void GenerateSpaProxyRoutes(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;

        app.UseRouting();
        app.UseEndpoints(b =>
        {
            var sources = b.DataSources;
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
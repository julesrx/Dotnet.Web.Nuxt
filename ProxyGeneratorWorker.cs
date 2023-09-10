using System.Diagnostics;
using System.Text.Json;

namespace Dotnet.Web.Nuxt;

public sealed class ProxyGeneratorWorker : BackgroundService
{
    private readonly IHostApplicationLifetime _lifetime;
    private readonly IWebHostEnvironment _env;
    private readonly IEnumerable<EndpointDataSource> _endpointSources;

    public ProxyGeneratorWorker(IWebHostEnvironment env, IEnumerable<EndpointDataSource> endpointSources,
        IHostApplicationLifetime lifetime)
    {
        _env = env;
        _endpointSources = endpointSources;
        _lifetime = lifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!_lifetime.ApplicationStarted.IsCancellationRequested)
        {
            await GenerateProxyRoutes();
            await Task.Delay(1000, ct);
        }
    }

    private async Task GenerateProxyRoutes()
    {
        var routes = _endpointSources
            .SelectMany(s => s.Endpoints)
            .Where(e => e is RouteEndpoint)
            .Cast<RouteEndpoint>();

        var paths = routes
            .Select(r => r.RoutePattern.RawText)
            .Where(t => !string.IsNullOrEmpty(t) && !t.Contains("nonfile"))
            .Distinct()
            .ToList();

        var filename = Path.Combine(_env.ContentRootPath, "ClientApp/proxy-paths.json");
        var json = JsonSerializer.Serialize(
            paths, new JsonSerializerOptions(JsonSerializerOptions.Default) { WriteIndented = true }
        );

        await File.WriteAllTextAsync(filename, json);
    }
}

public static class ProxyGenerator
{
    [Conditional("DEBUG")]
    public static void GenerateSpaProxyRoutes(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<ProxyGeneratorWorker>();
    }
}
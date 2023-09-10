using Dotnet.Web.Nuxt;

var builder = WebApplication.CreateBuilder(args);

builder.GenerateSpaProxyRoutes();

var app = builder.Build();

app.MapGet("/hello", () => "Hello World!");

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.Run();
using Dotnet.Web.Nuxt;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/data", () => "Hello World!");

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.GenerateSpaProxyRoutes();

app.Run();
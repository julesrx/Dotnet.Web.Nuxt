var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/_/hello", () => "Hello World!");

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.Run();
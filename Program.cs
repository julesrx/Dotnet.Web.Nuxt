using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

app.MapGet("/_/hello", () => "Hello World!").RequireAuthorization();

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.Run();
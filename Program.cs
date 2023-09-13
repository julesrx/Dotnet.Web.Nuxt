using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        JwtBearerDefaults.AuthenticationScheme,
        o => o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:Key")!)
            ),
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        }
    );

var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

app.MapGet("/_/hello", () => "Hello World!").RequireAuthorization();

app.MapPost("/_/auth/token", (IConfiguration config) =>
{
    var key = Encoding.UTF8.GetBytes(config.GetValue<string>("JwtSettings:Key")!);

    // Get this from the request and validate with a password 
    const string email = "test@example.com";

    var claims = new Claim[]
    {
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(JwtRegisteredClaimNames.Sub, email),
        new(JwtRegisteredClaimNames.Email, email)
    };

    var validity = TimeSpan.FromHours(1);
    var descriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.Add(validity),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature
        )
    };

    var handler = new JwtSecurityTokenHandler();
    var token = handler.CreateToken(descriptor);

    return handler.WriteToken(token);
});

app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.Run();
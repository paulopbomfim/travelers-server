using System.Text;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Travelers.Api;
using Travelers.Application;
using Travelers.Infrastructure;
using Travelers.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

var signingKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey");

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddApplication();

builder.Services
    .AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(config =>
    {
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = new TimeSpan(0),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
        };
    });

builder.Services.AddAuthorization();

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument(c =>
    {
        c.DocumentSettings = s =>
        {
            s.Title = ApiConstants.ApiTitle;
            s.Version = $"v{ApiConstants.ApiVersion}";
            s.DocumentName = ApiConstants.ApiTitle;
        };
    });


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app
    .UseAuthentication()
    .UseAuthorization();

app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.UseHttpsRedirection();

await MigrateDatabase();
app.Run();

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();
    await DataBaseMigration.MigrateDatabase(scope.ServiceProvider);
}

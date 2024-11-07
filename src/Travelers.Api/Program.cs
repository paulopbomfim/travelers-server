using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
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
    .AddAuthenticationJwtBearer(s => s.SigningKey = signingKey)
    .AddAuthorization()
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
    .UseAuthorization()
    .UseFastEndpoints();

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

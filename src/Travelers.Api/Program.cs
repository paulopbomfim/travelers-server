using FastEndpoints;
using FastEndpoints.Swagger;
using Travelers.Api;
using Travelers.Infrastructure;
using Travelers.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
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

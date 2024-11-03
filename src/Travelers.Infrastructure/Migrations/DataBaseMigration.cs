using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Travelers.Infrastructure.DataAccess;

namespace Travelers.Infrastructure.Migrations;

public static class DataBaseMigration
{
    public static async Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<TravelersDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
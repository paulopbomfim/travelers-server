using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Travelers.Domain.Interfaces.Repositories;
using Travelers.Domain.Interfaces.Security.Cryptography;
using Travelers.Domain.Repositories;
using Travelers.Infrastructure.DataAccess;
using Travelers.Infrastructure.DataAccess.Repositories;

namespace Travelers.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncryptor, Security.Cryptography.BCrypt>();
        
        AddRepositories(services);
        AddDbContext(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        #region '  Users Repository  '
            services.AddScoped<IUserReadOnlyRepository, UsersRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UsersRepository>();
        #endregion
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySqlConnection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<TravelersDbContext>(config =>
            config.UseMySql(connectionString, serverVersion));
    }
}
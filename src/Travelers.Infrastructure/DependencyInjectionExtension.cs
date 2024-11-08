using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Travelers.Domain.Interfaces.Repositories;
using Travelers.Domain.Interfaces.Security.Cryptography;
using Travelers.Domain.Interfaces.Security.Token;
using Travelers.Domain.Repositories;
using Travelers.Infrastructure.DataAccess;
using Travelers.Infrastructure.DataAccess.Repositories;
using Travelers.Infrastructure.Security.Token;

namespace Travelers.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncryptor, Security.Cryptography.BCrypt>();
        
        AddToken(services, configuration);
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
    
    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(signingKey!, expirationTimeMinutes));
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MySqlConnection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<TravelersDbContext>(config =>
            config.UseMySql(connectionString, serverVersion));
    }
}
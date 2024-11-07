using Microsoft.Extensions.DependencyInjection;
using Travelers.Application.UseCases.User;

namespace Travelers.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        #region '   User use cases  '
        
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        
        #endregion
    }
}
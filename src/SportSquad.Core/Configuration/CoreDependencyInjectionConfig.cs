using Microsoft.Extensions.DependencyInjection;
using SportSquad.Core.Interfaces;
using SportSquad.Core.Mediator;

namespace SportSquad.Core.Configuration;

public static class CoreDependencyInjectionConfig
{
    public static void DependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
    }
}
using Microsoft.Extensions.DependencyInjection;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Core.Interfaces;
using SportSquad.Data.Repositories;

namespace SportSquad.Data.Configuration;

public static class DataInjectionConfiguration
{
    public static void DependencyInjection(this IServiceCollection services)
    {
        InjectionDependencyRepository(services);
        InjectionDependencyUniOfWork(services);
    }

    private static void InjectionDependencyRepository(IServiceCollection services)
    {
        services.AddScoped<ICreateUserRepository, CreateUserRepository>();
        services.AddScoped<ICreateSquadRepository, CreateSquadRepository>();
        services.AddScoped<IPlayerTypeRepository, PlayerTypeRepository>();
        services.AddScoped<ICreatePlayerRepository, CreatePlayerRepository>();
        services.AddScoped<IUpdatePlayerRepository, UpdatePlayerRepository>();
    }
    
    private static void InjectionDependencyUniOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
using Microsoft.Extensions.DependencyInjection;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Data.Repositories;
using SportSquad.Domain.Models;

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
        services.AddScoped<ISquadRepository, SquadRepository>();
        services.AddScoped<IPlayerTypeRepository, PlayerTypeRepository>();
    }
    
    private static void InjectionDependencyUniOfWork(IServiceCollection services)
    {

    }
}
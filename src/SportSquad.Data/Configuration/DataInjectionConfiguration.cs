﻿using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<ISquadRepository, SquadRepository>();
        services.AddScoped<IPlayerTypeRepository, PlayerTypeRepository>();
    }
    
    private static void InjectionDependencyUniOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
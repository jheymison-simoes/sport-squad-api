using FluentValidation;
using MediatR;
using SportSquad.Core.Command;

namespace SportSquad.Api.Configuration;

public static class MediatrConfig
{
    public static void AddMediatRConfig(this IServiceCollection services)
    {
        const string applicationAssemblyName = "SportSquad.Business";
        var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

        AssemblyScanner
            .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandPipelineValidationBehavior<,>));

        services.AddMediatR(typeof(Startup));
    }
}
using System.Globalization;
using System.Resources;
using SportSquad.Business.Configuration;
using SportSquad.Core.Configuration;
using SportSquad.Core.Resource;
using SportSquad.Data.Configuration;

namespace SportSquad.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection DependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddSingleton(new ResourceManager(typeof(ApiResource)));
        services.AddScoped(provider =>
        {
            var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;

            if (httpContext == null)
            {
                return CultureInfo.InvariantCulture;
            }

            return httpContext.Request.Headers.TryGetValue("language", out var language)
                ? new CultureInfo(language)
                : CultureInfo.InvariantCulture;
        });
        
        BussinessDependencyInjectionConfig.DependencyInjection(services);
        DataInjectionConfiguration.DependencyInjection(services);
        CoreDependencyInjectionConfig.DependencyInjection(services);

        return services;
    }
}
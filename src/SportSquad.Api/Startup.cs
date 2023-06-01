using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using SportSquad.Api.Configuration;
using SportSquad.Data;

namespace SportSquad.Api;

public class Startup
{
    private IConfiguration Configuration { get; }
        
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiConfiguration(Configuration);
        services.AddResourceConfiguration();
        services.AddAuthenticatedJwt(Configuration);
        services.AddSwaggerConfiguration();
        services.AddMediatRConfig();
        services.DependencyInjection(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,  IApiVersionDescriptionProvider provider, SqlContext context)
    {
        context.Database.Migrate();
        app.UseApiConfiguration(env, Configuration);
        app.UseSwaggerConfiguration(provider);
    }
}

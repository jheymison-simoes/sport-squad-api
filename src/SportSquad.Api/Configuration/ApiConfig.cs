using System.Reflection;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportSquad.Api.Middlewares;
using SportSquad.Business.Models;
using SportSquad.Data;

namespace SportSquad.Api.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = configuration.Get<AppSettings>();

        services.AddKissLogConfiguration();
        
        services.AddControllers();

        services.AddHealthChecks().AddDbContextCheck<SqlContext>();
        
        services.AddDbContext<SqlContext>(options =>
        {
            options
                .UseNpgsql(appSettings.DbConnection,
                    builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
        });
            
        services.AddMemoryCache();

        services.AddAutoMapper(typeof(Startup));

        services.AddHttpContextAccessor();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });
        
        services.AddApiVersioning(p =>
        {
            p.DefaultApiVersion = new ApiVersion(1, 0);
            p.ReportApiVersions = true;
            p.AssumeDefaultVersionWhenUnspecified = true;
        });

        services.AddVersionedApiExplorer(p =>
        {
            p.GroupNameFormat = "'v'VVV";
            p.SubstituteApiVersionInUrl = true;
        });
    }
    
    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
                        
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors("Total");
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseKissLogConfiguration(configuration);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health/startup");
            endpoints.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = _ => false });
            endpoints.MapControllers();
        });

        return app;
    }
}
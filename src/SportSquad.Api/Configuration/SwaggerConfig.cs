using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SportSquad.Api.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Project API Sport Squad",
                    Description = "API Sport Squad",
                    Contact = new OpenApiContact() {Name = "Jheymison", Email = "jheymis1574@gmail.com"},
                    Version = "v1",
                });

            c.OperationFilter<AddAuthHeaderOperationFilter>();
            
            c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Description = "`Token apenas!!!` - sem o prefixo `Bearer_`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
        });
            
        //     c.SwaggerDoc("v1", new OpenApiInfo
        //     {
        //         Title = "Project API Your Sport",
        //         Description = "API Your Sport",
        //         Contact = new OpenApiContact() {Name = "Jheymison", Email = "jheymis1574@gmail.com"}
        //     });
        //         
        //     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //     {
        //         Description = "Enter the JWT token like this: Bearer {your token}",
        //         Name = "Authorization",
        //         Scheme = "Bearer",
        //         BearerFormat = "JWT",
        //         In = ParameterLocation.Header,
        //         Type = SecuritySchemeType.ApiKey
        //     });
        //     c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //     {
        //         {
        //             new OpenApiSecurityScheme
        //             {
        //                 Reference = new OpenApiReference
        //                 {
        //                     Type = ReferenceType.SecurityScheme,
        //                     Id = "Bearer"
        //                 }
        //             },
        //             new string[] {}
        //         }
        //     });
        // });

        return services;
    }
    
    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
            c.DocExpansion(DocExpansion.List);
        });

        return app;
    }
    
    private class AddAuthHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var isAuthorized = (context.MethodInfo.DeclaringType!.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                                && !context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any()) //this excludes controllers with AllowAnonymous attribute in case base controller has Authorize attribute
                               || (context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                                   && !context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any()); // this excludes methods with AllowAnonymous attribute

            if (!isAuthorized) return;

            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

            var jwtbearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }

            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement { [jwtbearerScheme] = new string []{} }
            };
        }
    }
}


using System.Text;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportSquad.Business.Models;

namespace SportSquad.Api.Configuration;

public static class AuthenticatedConfiguration
{
    public static void AddAuthenticatedJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);
        var appSettings = appSettingsSection.Get<AppSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings.SecretToken);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = "SportSquad";
            x.DefaultChallengeScheme = "SportSquad";
        }).AddJwtBearer("SportSquad",x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        })
        .AddGoogle(GoogleDefaults.AuthenticationScheme, x =>
        {
            x.ClientId = appSettings.GoogleClientId;
            x.ClientSecret = appSettings.GoogleClientSecret;
        });
        
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes("SportSquad", "Google")
                .Build();
        });
        
        
        // var key = Encoding.ASCII.GetBytes(appSettings.SecretToken);
        // services.AddAuthentication(options =>
        // {
        //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // }).AddJwtBearer(options =>
        // {
        //     options.RequireHttpsMetadata = false;
        //     options.SaveToken = true;
        //     options.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = new SymmetricSecurityKey(key),
        //         ValidateIssuer = false,
        //         ValidateAudience = false
        //     };
        // });
    }
}
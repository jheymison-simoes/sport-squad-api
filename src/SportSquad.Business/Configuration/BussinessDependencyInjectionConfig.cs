using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Services;
using SportSquad.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Models.User.Request;

namespace SportSquad.Business.Configuration
{
    public static class BussinessDependencyInjectionConfig
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.DependencyInjectionServices();
            services.DependencyInjectionValidators();
        }

        private static void DependencyInjectionValidators(this IServiceCollection services)
        {
            #region Bussiness
            services.AddScoped<LoginRequestValidator>();
            services.AddScoped<CreateUserRequestValidator>();
            services.AddScoped<CreateSquadValidator>();
            services.AddScoped<CreateSquadSquadConfigValidator>();
            services.AddScoped<LoginWithGoogleRequestValidator>();
            services.AddScoped<CreateUserWithGoogleRequest>();
            #endregion

            #region Domain
            services.AddScoped<UserValidator>();
            services.AddScoped<PlayerValidator>();
            services.AddScoped<SquadValidator>();
            services.AddScoped<SquadConfigValidator>();
            services.AddScoped<PlayerTypeValidator>();
            #endregion
        }

        private static void DependencyInjectionServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticatedService, AuthenticatedService>();
            services.AddScoped<IEncryptService, EncryptService>();
            services.AddScoped<ICreateUserService, CreateUserService>();
            services.AddScoped<ISquadService, SquadService>();
        }
    }
}

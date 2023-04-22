﻿using MediatR;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Services;
using SportSquad.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using SportSquad.Business.Commands.Authentication;
using SportSquad.Business.Commands.Squad;
using SportSquad.Business.Commands.User;
using SportSquad.Business.Handlers;
using SportSquad.Business.Handlers.Squad;
using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Models.User.Response;
using SportSquad.Core.Command;

namespace SportSquad.Business.Configuration
{
    public static class BussinessDependencyInjectionConfig
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.DependencyInjectionServices();
            services.DependencyInjectionCommandHandlers();
            services.DependencyInjectionValidators();
        }

        private static void DependencyInjectionValidators(this IServiceCollection services)
        {
            #region Bussiness
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
            services.AddScoped<LoginCommandValidator>();
            #endregion
        }

        private static void DependencyInjectionServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthenticatedService, AuthenticatedService>();
            services.AddScoped<IEncryptService, EncryptService>();
        }
        
        private static void DependencyInjectionCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<LoginCommand, CommandResponse<UserSessionResponse>>, AuthenticationCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, CommandResponse<UserResponse>>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserWithGoogleCommand, CommandResponse<UserResponse>>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateSquadCommand, CommandResponse<SquadResponse>>, CreateSquadCommandHandler>();
        }
    }
}
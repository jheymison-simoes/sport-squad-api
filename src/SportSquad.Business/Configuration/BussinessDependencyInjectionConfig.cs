using MediatR;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Services;
using SportSquad.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using SportSquad.Business.Commands.Authentication;
using SportSquad.Business.Commands.Squad;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Commands.Squad.Player.PlayerType;
using SportSquad.Business.Commands.Squad.SquadConfig;
using SportSquad.Business.Commands.User;
using SportSquad.Business.Handlers.Authentication;
using SportSquad.Business.Handlers.Player;
using SportSquad.Business.Handlers.Squad;
using SportSquad.Business.Handlers.User;
using SportSquad.Business.Interfaces.Strategies;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Models.PlayerType;
using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Models.User.Response;
using SportSquad.Business.Strategies;
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
            services.AddScoped<IAssembleTeamsStrategyBalancedStrategy, AssembleTeamsStrategyBalancedStrategy>();
            services.AddScoped<IAssembleTeamsStrategyNotBalancedStrategy, AssembleTeamsStrategyNotBalancedStrategyStrategy>();
        }
        
        private static void DependencyInjectionCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<LoginCommand, CommandResponse<UserSessionResponse>>, AuthenticationCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, CommandResponse<UserResponse>>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserWithGoogleCommand, CommandResponse<UserResponse>>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateSquadCommand, CommandResponse<SquadResponse>>, CreateSquadCommandHandler>();
            services.AddScoped<IRequestHandler<CreatePlayerCommand, CommandResponse<PlayerResponse>>, CreatePlayerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdatePlayerCommand, CommandResponse<PlayerResponse>>, UpdatePlayerCommandHandler>();
            services.AddScoped<IRequestHandler<DeletePlayerCommand, CommandResponse<PlayerResponse>>, DeletePlayerCommandHandler>();
            services.AddScoped<IRequestHandler<GetAllPlayerCommand, CommandResponse<IEnumerable<PlayerResponse>>>, GetPlayerCommandHandler>();
            services.AddScoped<IRequestHandler<GetAllPlayerBySquadIdCommand, CommandResponse<List<PlayerGroupedTypeResponse>>>, GetPlayerCommandHandler>();
            services.AddScoped<IRequestHandler<GetPlayerByIdCommand, CommandResponse<PlayerResponse>>, GetPlayerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateSquadConfigCommand, CommandResponse<SquadConfigResponse>>, UpdateSquadCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteSquadConfigCommand, CommandResponse<SquadConfigResponse>>, DeleteSquadCommandHandler>();
            services.AddScoped<IRequestHandler<GetAllSquadByUserCommand, CommandResponse<IEnumerable<SquadResponse>>>, GetSquadCommandHandler>();
            services.AddScoped<IRequestHandler<GetAllPlayerTypeCommand, CommandResponse<IEnumerable<PlayerTypeResponse>>>, GetPlayerCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteSquadByIdCommand, CommandResponse<SquadResponse>>, DeleteSquadCommandHandler>();
            services.AddScoped<IRequestHandler<AssembleTeamsCommand, CommandResponse<List<AssembledTeamResponse>>>, AssemblyTeamsCommandHandler>();
            services.AddScoped<IRequestHandler<SharedTextAssembledTeamsCommand, CommandResponse<string>>, SharedTextAssembledTeamsCommandHandler>();
        }
    }
}

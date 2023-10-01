using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Squad;
using SportSquad.Business.Exceptions;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Interfaces.Strategies;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Squad;

public class AssemblyTeamsCommandHandler : BaseHandler, 
    IRequestHandler<AssembleTeamsCommand, CommandResponse<List<AssembledTeamResponse>>>
{
    #region Repositories
    private readonly IAssembleTeamsRepository _assembleTeamsRepository;
    #endregion

    #region Strategies
    private IAssembleTeamsStrategy _assembleTeamsStrategy;
    private readonly IAssembleTeamsStrategyBalancedStrategy _assembleTeamsStrategyBalancedStrategy;
    private readonly IAssembleTeamsStrategyNotBalancedStrategy _assembleTeamsStrategyNotBalancedStrategy;
    #endregion
    
    public AssemblyTeamsCommandHandler(
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager, 
        CultureInfo cultureInfo, 
        IAssembleTeamsRepository assembleTeamsRepository, 
        IAssembleTeamsStrategyBalancedStrategy assembleTeamsStrategyBalancedStrategy, 
        IAssembleTeamsStrategyNotBalancedStrategy assembleTeamsStrategyNotBalancedStrategy) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _assembleTeamsRepository = assembleTeamsRepository;
        _assembleTeamsStrategyBalancedStrategy = assembleTeamsStrategyBalancedStrategy;
        _assembleTeamsStrategyNotBalancedStrategy = assembleTeamsStrategyNotBalancedStrategy;
    }

    public async Task<CommandResponse<List<AssembledTeamResponse>>> Handle(AssembleTeamsCommand request, CancellationToken cancellationToken)
    {
        var squadExist = await _assembleTeamsRepository.Exists(s => s.Id == request.SquadId);
        if (!squadExist) return ReturnError<List<AssembledTeamResponse>>(ApiResource.SQUAD_NOT_FOUND_BY_ID, request.SquadId);

        var players = await _assembleTeamsRepository.GetPlayersBySquadIdAsync(request.SquadId);
        if (!players.Any()) return ReturnError<List<AssembledTeamResponse>>(ApiResource.SQUAD_WITHOUT_PLAYERS);
        
        var result = GenerateTeams(request, players);
        return !ValidOperation() ? ReturnReply<List<AssembledTeamResponse>>() : ReturnReply(result);
    }

    #region Private Methods

    private List<AssembledTeamResponse> GenerateTeams(AssembleTeamsCommand request, List<TeamResponse> players)
    {
        try
        {
            _assembleTeamsStrategy = request.Balanced switch
            {
                true => _assembleTeamsStrategyBalancedStrategy,
                _ => _assembleTeamsStrategyNotBalancedStrategy
            };

            var result = _assembleTeamsStrategy.Assemble(request.QuantityTeams, players);
            result.ForEach(s => s.SquadId = request.SquadId);

            return result;
        }
        catch (CustomException ex)
        {
            AddError(ex.Message);
            return new();
        }
    }
    #endregion
}
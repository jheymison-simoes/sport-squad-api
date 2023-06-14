using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Commands.Squad.Player.PlayerType;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Models.PlayerType;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Player;

public class GetPlayerCommandHandler : BaseHandler,
    IRequestHandler<GetAllPlayerCommand, CommandResponse<IEnumerable<PlayerResponse>>>,
    IRequestHandler<GetPlayerByIdCommand, CommandResponse<PlayerResponse>>,
    IRequestHandler<GetAllPlayerTypeCommand, CommandResponse<IEnumerable<PlayerTypeResponse>>>, 
    IRequestHandler<GetAllPlayerBySquadIdCommand, CommandResponse<List<PlayerGroupedTypeResponse>>>
{
    #region Repositories
    private readonly IGetPlayerRepository _getPlayerRepository;
    #endregion
    
    public GetPlayerCommandHandler(
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo, 
        IGetPlayerRepository getPlayerRepository) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _getPlayerRepository = getPlayerRepository;
    }

    public async Task<CommandResponse<IEnumerable<PlayerResponse>>> Handle(GetAllPlayerCommand request, CancellationToken cancellationToken)
    {
        var players = await _getPlayerRepository.GetAllAsync(request.SquadId);
        return ReturnReply(players);
    }

    public async Task<CommandResponse<PlayerResponse>> Handle(GetPlayerByIdCommand request, CancellationToken cancellationToken)
    {
        var player = await _getPlayerRepository.GetById(request.Id);
        if (player is null) return ReturnError<PlayerResponse>(ApiResource.PLAYER_NOT_FOUND_BY_ID, request.Id);

        var response = Mapper.Map<PlayerResponse>(player);
        return ReturnReply(response);
    }

    public async Task<CommandResponse<IEnumerable<PlayerTypeResponse>>> Handle(GetAllPlayerTypeCommand request, CancellationToken cancellationToken)
    {
        var playersTypes = await _getPlayerRepository.GetAllPlayersTypesAsync();
        return ReturnReply(playersTypes);
    }

    public async Task<CommandResponse<List<PlayerGroupedTypeResponse>>> Handle(GetAllPlayerBySquadIdCommand request, CancellationToken cancellationToken)
    {
        var players = (await _getPlayerRepository.GetAllBySquadIdGroupedAsync(request.SquadId)).ToList();
        if (!players.Any()) return ReturnReply(players);
        
        foreach (var playerType in players)
        {
            for (var i = 0; i < playerType.Players.Count(); i++)
            {
                var player = playerType.Players.ElementAt(i);
                player.Index = i + 1;
                if (playerType.QuantityMaxPlayers >= player.Index) continue;
                player.Substitute = true;
            }
        }
        
        return ReturnReply(players);
    }
}
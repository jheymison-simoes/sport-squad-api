using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Player;

public class UpdatePlayerCommandHandler : BaseHandler, 
    IRequestHandler<UpdatePlayerCommand, CommandResponse<PlayerResponse>>
{
    #region Repositories
    private readonly IUpdatePlayerRepository _updatePlayerRepository;
    #endregion
    
    public UpdatePlayerCommandHandler(
        IMapper mapper,
        AppSettings appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        IUpdatePlayerRepository updatePlayerRepository) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _updatePlayerRepository = updatePlayerRepository;
    }

    public async Task<CommandResponse<PlayerResponse>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _updatePlayerRepository.GetById(request.Id);
        if (player is null) return ReturnError<PlayerResponse>(ApiResource.PLAYER_NOT_FOUND_BY_ID, request.Id);

        var asSame = AsSame(request, player);
        if (asSame) return ReturnReply(Mapper.Map<PlayerResponse>(player));
        
        var existsPlayerType = await _updatePlayerRepository.ExistsPlayerType(request.PlayerTypeId);
        if (!existsPlayerType) return ReturnError<PlayerResponse>(ApiResource.PLAYER_TYPE_NOT_FOUND_BY_ID, request.PlayerTypeId);

        await CheckMaxPlayersSquadAsync(player.SquadId, request.PlayerTypeId);
        if (!ValidOperation()) return ReturnReply<PlayerResponse>();
        
        player.PlayerTypeId = request.PlayerTypeId;
        player.Name = request.Name;
        player.UserId = request.UserId;
        player.SkillLevel = request.SkillLevel;
        
        _updatePlayerRepository.Update(player);
        await SaveData(_updatePlayerRepository.UnitOfWork);

        var response = Mapper.Map<PlayerResponse>(player);
        return ReturnReply(response);
    }

    #region Private Methods
    private bool AsSame(UpdatePlayerCommand request, Domain.Models.Player player)
    {
        return request.SkillLevel == player.SkillLevel &&
               request.PlayerTypeId == player.PlayerTypeId &&
               request.Name == player.Name;
    }
    
    private async Task CheckMaxPlayersSquadAsync(Guid squadId, Guid playerTypeId)
    {
        var squadConfig = await _updatePlayerRepository.GetSquadConfigBySquadIdAsync(squadId, playerTypeId);
        if (squadConfig is null) return;

        var quantityPlayer = await _updatePlayerRepository.GetQuantityPlayersSquadAsync(squadId, playerTypeId);
        if (quantityPlayer < squadConfig.QuantityPlayers || squadConfig.AllowSubstitutes) return;

        AddErrorResource(ApiResource.SQUAD_EXCEEDED_MAX_PLAYERS);
    }
    #endregion
}
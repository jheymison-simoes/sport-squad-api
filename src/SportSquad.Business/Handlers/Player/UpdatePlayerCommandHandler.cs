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
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        IUpdatePlayerRepository updatePlayerRepository) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _updatePlayerRepository = updatePlayerRepository;
    }

    public async Task<CommandResponse<PlayerResponse>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _updatePlayerRepository.GetById(request.Id);
        if (player is null) return ReturnReplyWithError<PlayerResponse>(ApiResource.PLAYER_NOT_FOUND_BY_ID, request.Id);

        var asSame = request.AsSame(player.Name, player.PlayerTypeId);
        if (asSame) return ReturnReply(Mapper.Map<PlayerResponse>(player));
        
        var nameIsDuplicated = await _updatePlayerRepository.IsDuplicated(request.Name, player.SquadId);
        if (nameIsDuplicated) return ReturnReplyWithError<PlayerResponse>(ApiResource.SQUAD_PLAYER_NAME_DUPLICATED);

        var existsPlayerType = await _updatePlayerRepository.ExistsPlayerType(request.PlayerTypeId);
        if (!existsPlayerType) return ReturnReplyWithError<PlayerResponse>(ApiResource.PLAYER_TYPE_NOT_FOUND_BY_ID, request.PlayerTypeId);

        player.PlayerTypeId = request.PlayerTypeId;
        player.Name = request.Name;
        player.UserId = request.UserId;
        
        _updatePlayerRepository.Update(player);
        await SaveData(_updatePlayerRepository.UnitOfWork);

        var response = Mapper.Map<PlayerResponse>(player);
        return ReturnReply(response);
    }
}
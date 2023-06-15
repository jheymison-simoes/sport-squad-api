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
using SportSquad.Domain.Models;
using PlayerDomain = SportSquad.Domain.Models.Player;

namespace SportSquad.Business.Handlers.Player;

public class CreatePlayerCommandHandler : BaseHandler, 
    IRequestHandler<CreatePlayerCommand, CommandResponse<PlayerResponse>>
{
    #region Repositories
    private readonly ICreatePlayerRepository _createPlayerRepository;
    #endregion

    #region Validators
    private readonly PlayerValidator _playerValidator;
    #endregion
    
    public CreatePlayerCommandHandler(
        IMapper mapper,
        IOptions<AppSettings> appSettings, 
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        ICreatePlayerRepository createPlayerRepository,
        PlayerValidator playerValidator) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _createPlayerRepository = createPlayerRepository;
        _playerValidator = playerValidator;
    }

    public async Task<CommandResponse<PlayerResponse>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var isDuplicated = await _createPlayerRepository.IsDuplicatedAync(request.Name, request.SquadId);
        if (isDuplicated) return ReturnError<PlayerResponse>(ApiResource.SQUAD_PLAYER_NAME_DUPLICATED);

        var squadExists = await _createPlayerRepository.ExistsSquadAsync(request.SquadId);
        if (!squadExists) return ReturnError<PlayerResponse>(ApiResource.SQUAD_NOT_FOUND_BY_ID, request.SquadId);
        
        var playerTypeExists = await _createPlayerRepository.ExistsPlayerTypeAsync(request.PlayerTypeId);
        if (!playerTypeExists) return ReturnError<PlayerResponse>(ApiResource.PLAYER_TYPE_NOT_FOUND_BY_ID, request.PlayerTypeId);

        await CheckMaxPlayersSquadAsync(request.SquadId, request.PlayerTypeId);
        if (!ValidOperation()) return ReturnReply<PlayerResponse>();
        
        var player = Mapper.Map<PlayerDomain>(request);

        player.UserId = player.UserId == default(Guid) ? null : player.UserId;

        await _playerValidator.ValidateAsync(player, cancellationToken);
        if (!ValidOperation()) return ReturnReply<PlayerResponse>();
        
        _createPlayerRepository.Add(player);
        await SaveData(_createPlayerRepository.UnitOfWork);

        var response = Mapper.Map<PlayerResponse>(player);
        return ReturnReply(response);
    }

    #region Private Methods
    private async Task CheckMaxPlayersSquadAsync(Guid squadId, Guid playerTypeId)
    {
        var squadConfig = await _createPlayerRepository.GetSquadConfigBySquadIdAsync(squadId, playerTypeId);
        if (squadConfig is null) return;

        var quantityPlayer = await _createPlayerRepository.GetQuantityPlayersSquadAsync(squadId, playerTypeId);
        if (quantityPlayer < squadConfig.QuantityPlayers || squadConfig.AllowSubstitutes) return;

        AddErrorResource(ApiResource.SQUAD_EXCEEDED_MAX_PLAYERS);
    }
    #endregion
}
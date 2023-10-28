using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using SportSquad.Business.Commands.Squad.Player.PlayerType;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models;
using SportSquad.Business.Models.PlayerType;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Player;

public class GetAllPlayerTypeBySquadIdCommandHandler : BaseHandler, 
    IRequestHandler<GetAllPlayerTypeBySquadIdCommand, CommandResponse<List<PlayerTypeResponse>>>
{
    #region Repositories
    private readonly IGetPlayerTypeRepository _getPlayerTypeRepository;
    #endregion
    
    public GetAllPlayerTypeBySquadIdCommandHandler(
        IMapper mapper,
        AppSettings appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo, 
        IGetPlayerTypeRepository getPlayerTypeRepository
    ) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _getPlayerTypeRepository = getPlayerTypeRepository;
    }

    public async Task<CommandResponse<List<PlayerTypeResponse>>> Handle(GetAllPlayerTypeBySquadIdCommand request, CancellationToken cancellationToken)
    {
        var squadExist = await _getPlayerTypeRepository.ExistSquadAsync(request.SquadId);
        if (!squadExist) return ReturnError<List<PlayerTypeResponse>>(ApiResource.SQUAD_NOT_FOUND_BY_ID, request.SquadId);

        var playersTypes = await _getPlayerTypeRepository.GetAllBySquadIdAsync(request.SquadId);
        return ReturnReply(playersTypes);
    }
}
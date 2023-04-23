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

namespace SportSquad.Business.Handlers.Player;

public class GetPlayerCommandHandler : BaseHandler,
    IRequestHandler<GetAllPlayerCommand, CommandResponse<IEnumerable<PlayerResponse>>>
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
        var players = await _getPlayerRepository.GetAll(request.SquadId);
        return ReturnReply(players);
    }
}
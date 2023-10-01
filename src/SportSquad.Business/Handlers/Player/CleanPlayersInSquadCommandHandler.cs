using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Player;

public class CleanPlayersInSquadCommandHandler : BaseHandler, 
    IRequestHandler<CleanPlayersInSquadCommand, CommandResponse<bool>>
{
    #region Repositories
    private readonly ICleanPlayersInSquadRepository _cleanPlayersInSquadRepository;
    #endregion
    
    public CleanPlayersInSquadCommandHandler(
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        ICleanPlayersInSquadRepository cleanPlayersInSquadRepository
        ) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _cleanPlayersInSquadRepository = cleanPlayersInSquadRepository;
    }

    public async Task<CommandResponse<bool>> Handle(CleanPlayersInSquadCommand request, CancellationToken cancellationToken)
    {
        var squadExist = await _cleanPlayersInSquadRepository.Exists(s => s.Id == request.SquadId);
        if (!squadExist) return ReturnError<bool>(ApiResource.SQUAD_NOT_FOUND_BY_ID, request.SquadId);

        await _cleanPlayersInSquadRepository.RemoveAllPlayerAsync(request.SquadId);
        
        return ReturnReply(true);
    }
}
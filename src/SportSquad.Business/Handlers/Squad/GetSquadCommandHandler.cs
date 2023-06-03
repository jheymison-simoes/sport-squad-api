using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Squad;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Core.Command;

namespace SportSquad.Business.Handlers.Squad;

public class GetSquadCommandHandler : BaseHandler,
    IRequestHandler<GetAllSquadByUserCommand, CommandResponse<IEnumerable<SquadResponse>>>
{
    #region Repositories
    private readonly IGetSquadRepository _getSquadRepository;
    #endregion
    
    public GetSquadCommandHandler(
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo, 
        IGetSquadRepository getSquadRepository) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _getSquadRepository = getSquadRepository;
    }

    public async Task<CommandResponse<IEnumerable<SquadResponse>>> Handle(GetAllSquadByUserCommand request, CancellationToken cancellationToken)
    {
        var squads = await _getSquadRepository.GetAllByUserIdAsync(request.UserId);
        if (!squads.Any()) ReturnReply(new List<SquadResponse>());

        var response = Mapper.Map<IEnumerable<SquadResponse>>(squads);
        return ReturnReply(response);
    }
}
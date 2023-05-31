using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Squad.SquadConfig;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Squad;

public class DeleteSquadCommandHandler : BaseHandler, 
    IRequestHandler<DeleteSquadConfigCommand, CommandResponse<SquadConfigResponse>>
{
    #region Repositories
    private readonly IDeleteSquadRepository _deleteSquadRepository;
    #endregion
    
    public DeleteSquadCommandHandler(
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        IDeleteSquadRepository deleteSquadRepository) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _deleteSquadRepository = deleteSquadRepository;
    }

    public async Task<CommandResponse<SquadConfigResponse>> Handle(DeleteSquadConfigCommand request, CancellationToken cancellationToken)
    {
        var squadConfig = await _deleteSquadRepository.GetSquadConfigByIdAsync(request.Id);
        if (squadConfig is null) return ReturnError<SquadConfigResponse>(ApiResource.SQUAD_CONFIG_NOT_FOUND_BY_ID, request.Id);
        
        var playersLinkedToSquad = await _deleteSquadRepository.GetAllSquadPlayersAsync(squadConfig.SquadId, squadConfig.PlayerTypeId);
        if (playersLinkedToSquad.Any()) _deleteSquadRepository.DeletePlayers(playersLinkedToSquad);
        
        _deleteSquadRepository.DeleteSquadConfig(squadConfig);

        await SaveData(_deleteSquadRepository.UnitOfWork);

        var response = Mapper.Map<SquadConfigResponse>(squadConfig);
        return ReturnReply(response);
    }
}
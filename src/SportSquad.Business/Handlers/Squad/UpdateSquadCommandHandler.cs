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

public class UpdateSquadCommandHandler : BaseHandler, 
    IRequestHandler<UpdateSquadConfigCommand, CommandResponse<SquadConfigResponse>>
{
    #region Repositories
    private readonly IUpdateSquadRepository _updateSquadRepository;
    #endregion
    
    public UpdateSquadCommandHandler(
        IMapper mapper,
        AppSettings appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        IUpdateSquadRepository updateSquadRepository) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _updateSquadRepository = updateSquadRepository;
    }

    public async Task<CommandResponse<SquadConfigResponse>> Handle(UpdateSquadConfigCommand request, CancellationToken cancellationToken)
    {
        var squadConfig = await _updateSquadRepository.GetSquadConfigByIdAsync(request.Id);
        if (squadConfig is null) return ReturnError<SquadConfigResponse>(ApiResource.SQUAD_CONFIG_NOT_FOUND_BY_ID, request.Id);

        squadConfig.AllowSubstitutes = request.AllowSubstitutes;
        squadConfig.QuantityPlayers = request.QuantityPlayers;
        
        _updateSquadRepository.UpdateSquadConfig(squadConfig);
        await SaveData(_updateSquadRepository.UnitOfWork);

        var response = Mapper.Map<SquadConfigResponse>(squadConfig);
        return ReturnReply(response);
    }
}
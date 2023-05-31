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
using SportSquad.Core.Resource;
using SportSquad.Domain.Models;
using SquadDomain = SportSquad.Domain.Models.Squad;

namespace SportSquad.Business.Handlers.Squad;

public class CreateSquadCommandHandler : BaseHandler, 
    IRequestHandler<CreateSquadCommand, CommandResponse<SquadResponse>>
{
    #region Repositorios
    private readonly ICreateSquadRepository _createSquadRepository;
    #endregion

    #region Validators

    private readonly SquadValidator _squadValidator;

    #endregion
    
    public CreateSquadCommandHandler(
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager, 
        CultureInfo cultureInfo,
        ICreateSquadRepository createSquadRepository, 
        SquadValidator squadValidator) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _createSquadRepository = createSquadRepository;
        _squadValidator = squadValidator;
    }
    
    public async Task<CommandResponse<SquadResponse>> Handle(CreateSquadCommand request, CancellationToken cancellationToken)
    {
        var squadDuplicated = await _createSquadRepository.IsDuplicated(request.Name, request.UserId);
        if (squadDuplicated) return ReturnError<SquadResponse>(ApiResource.SQUAD_EXISTING_SQUAD, request.Name);

        var userExists = await _createSquadRepository.UserExists(request.UserId);
        if (!userExists) return ReturnError<SquadResponse>( ApiResource.USER_NOT_FOUND_BY_ID, request.UserId);

        var squadConfigs = request.SquadConfigs;
        foreach (var squadConfig in squadConfigs)
        {
            var playerTypeExist = await _createSquadRepository.PlayerTypeExists(squadConfig.PlayerTypeId);
            if (!playerTypeExist) return ReturnError<SquadResponse>(ApiResource.PLAYER_TYPE_NOT_FOUND_BY_ID, squadConfig.PlayerTypeId);
        }
        
        var squad = Mapper.Map<SquadDomain>(request);

        await _squadValidator.ValidateAsync(squad, cancellationToken);
        if (!ValidOperation()) return ReturnReply<SquadResponse>();
        
        _createSquadRepository.Add(squad);
        await _createSquadRepository.SaveChanges();
        
        var response = Mapper.Map<SquadResponse>(squad);
        return ReturnReply(response);
    }
}
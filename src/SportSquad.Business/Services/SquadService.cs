using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using SportSquad.Business.Exceptions;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Domain.Models;

namespace SportSquad.Business.Services;

public class SquadService : BaseService, ISquadService
{
    #region Repositories
    private readonly ISquadRepository _squadRepository;
    private readonly ICreateUserRepository _createUserRepository;
    private readonly IPlayerTypeRepository _playerTypeRepository;
    #endregion
    
    #region Validators
    private readonly CreateSquadValidator _createSquadValidator;
    private readonly SquadValidator _squadValidator;
    #endregion
    
    public SquadService(
        IMapper mapper, 
        IOptions<AppSettings> appSettings, 
        ResourceManager resourceManager, 
        CultureInfo cultureInfo, 
        ISquadRepository squadRepository, 
        ICreateUserRepository createUserRepository,
        IPlayerTypeRepository playerTypeRepository,
        CreateSquadValidator createSquadValidator, 
        SquadValidator squadValidator) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _squadRepository = squadRepository;
        _createSquadValidator = createSquadValidator;
        _squadValidator = squadValidator;
        _playerTypeRepository = playerTypeRepository;
        _createUserRepository = createUserRepository;
    }

    public async Task<SquadResponse> CreateSquad(CreateSquadRequest request)
    {
        var requestValidator = await _createSquadValidator.ValidateModel(request);
        if (requestValidator.error) ReturnError<CustomException>(requestValidator.messageError);

        var squadDuplicated = await _squadRepository.IsDuplicated(request.Name, request.UserId);
        if (squadDuplicated) ReturnResourceError<CustomException>("SQUAD-EXISTING_SQUAD", request.Name);

        var userExists = await _createUserRepository.Exists(u => u.Id == request.UserId);
        if (!userExists) ReturnResourceError<CustomException>("USER-NOT_FOUND_BY_ID", request.UserId);

        var squadConfigs = request.SquadConfigs;
        foreach (var squadConfig in squadConfigs)
        {
            var playerTypeExist = await _playerTypeRepository.Exists(pt => pt.Id == squadConfig.PlayerTypeId);
            if (!playerTypeExist) ReturnResourceError<CustomException>("PLAYER-TYPE-NOT_FOUND_BY_ID", squadConfig.PlayerTypeId);
        }
        
        var squad = Mapper.Map<Squad>(request);

        var squadValidator = await _squadValidator.ValidateModel(squad);
        if (squadValidator.error) ReturnError<CustomException>(squadValidator.messageError);
        
        _squadRepository.Add(squad);
        await _squadRepository.SaveChanges();
        
        return Mapper.Map<SquadResponse>(squad);
    }
}
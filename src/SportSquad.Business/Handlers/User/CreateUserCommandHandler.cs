using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.User;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models;
using SportSquad.Business.Models.User.Response;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;
using SportSquad.Domain.Models;

namespace SportSquad.Business.Handlers.User;

public class CreateUserCommandHandler : BaseHandler,
    IRequestHandler<CreateUserCommand, CommandResponse<UserResponse>>,
    IRequestHandler<CreateUserWithGoogleCommand, CommandResponse<UserResponse>>
{
    #region Repositories
    private readonly ICreateUserRepository _createUserRepository;
    #endregion

    #region Services
    private readonly IEncryptService _encryptService;
    #endregion

    #region Validators
    private readonly UserValidator _userValidator;
    #endregion
    
    public CreateUserCommandHandler(
        IMapper mapper,
        AppSettings appSettings,
        ResourceManager resourceManager, 
        CultureInfo cultureInfo, 
        ICreateUserRepository createUserRepository, 
        IEncryptService encryptService,
        UserValidator userValidator) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _createUserRepository = createUserRepository;
        _encryptService = encryptService;
        _userValidator = userValidator;
    }
    
    public async Task<CommandResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userDuplicated = await _createUserRepository.IsDuplicated(request.Email);
        if (userDuplicated) return ReturnError<UserResponse>(ApiResource.USER_EXISTING_USER);
        
        var user = Mapper.Map<Domain.Models.User>(request);
        user.Password = _encryptService.EncryptPassword(user.Password);

        await _userValidator.ValidateAsync(user, cancellationToken);
        if (!ValidOperation()) return ReturnReply<UserResponse>();
        
        _createUserRepository.Add(user);
        await _createUserRepository.SaveChanges();

        var response = Mapper.Map<UserResponse>(user);
        return ReturnReply(response);
    }

    public async Task<CommandResponse<UserResponse>> Handle(CreateUserWithGoogleCommand request, CancellationToken cancellationToken)
    {
        var userDuplicated = await _createUserRepository.IsDuplicated(request.Email);
        if (userDuplicated) return ReturnError<UserResponse>(ApiResource.USER_EXISTING_USER);
        
        var user = Mapper.Map<Domain.Models.User>(request);
        user.Password = string.Empty;

        await _userValidator.ValidateAsync(user, cancellationToken);
        if (!ValidOperation()) return ReturnReply<UserResponse>();
        
        _createUserRepository.Add(user);
        await _createUserRepository.SaveChanges();

        var response = Mapper.Map<UserResponse>(user);
        return ReturnReply(response);
    }
}
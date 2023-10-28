using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using SportSquad.Business.Commands.Authentication;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models;
using SportSquad.Business.Models.User.Response;
using SportSquad.Business.Utils;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;
using SportSquad.Domain.Models;
using UserEntity = SportSquad.Domain.Models.User;

namespace SportSquad.Business.Handlers.Authentication;

public class AuthenticationCommandHandler : BaseHandler,
    IRequestHandler<LoginCommand, CommandResponse<UserSessionResponse>>, 
    IRequestHandler<LoginWithGoogleCommand, CommandResponse<UserSessionResponse>>
{
    private readonly ICreateUserRepository _createUserRepository;
    
    private readonly IEncryptService _encryptService;
    private readonly ITokenService _tokenService;

    #region Validators
    private readonly UserValidator _userValidator;
    #endregion
    
    public AuthenticationCommandHandler(
        IMapper mapper, 
        AppSettings appSettings,
        ResourceManager resourceManager, 
        CultureInfo cultureInfo,
        ICreateUserRepository createUserRepository, 
        IEncryptService encryptService,
        ITokenService tokenService, 
        UserValidator userValidator) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _createUserRepository = createUserRepository;
        _encryptService = encryptService;
        _tokenService = tokenService;
        _userValidator = userValidator;
    }
    
    public async Task<CommandResponse<UserSessionResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _createUserRepository.GetByEmail(request.Email);
        if (user is null) return ReturnError<UserSessionResponse>(ApiResource.USER_INVALID_LOGIN);
        
        var passwordEncrypt = _encryptService.EncryptPassword(request.Password);
        if (passwordEncrypt != user!.Password) return ReturnError<UserSessionResponse>(ApiResource.USER_INVALID_LOGIN);

        var response = GenerateUserSessionResponse(user);
        return ReturnReply(response);
    }

    public async Task<CommandResponse<UserSessionResponse>> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
    {
        var user = await GetOrCreateUserAsync(request);
        if (!ValidOperation()) ReturnReply<UserSessionResponse>();

        user.ImageUrl = request.PhotoUrl;
        
        var response = GenerateUserSessionResponse(user);
        return ReturnReply(response);
    }

    #region Private Methods
    private async Task<UserEntity> GetOrCreateUserAsync(LoginWithGoogleCommand request)
    {
        var user = await _createUserRepository.GetByEmail(request.Email);
        if (user is not null) return user;

        var newUser = new UserEntity(request.Name, request.Email);
        
        await _userValidator.ValidateAsync(newUser);
        if (!ValidOperation()) return null;
        
        _createUserRepository.Add(newUser);
        await _createUserRepository.SaveChanges();
        
        return newUser;
    }


    private UserSessionResponse GenerateUserSessionResponse(UserEntity user)
    {
        var tokenGenerated = _tokenService.GenerateToken(user);
        
        var response = new UserSessionResponse()
        {
            Token = tokenGenerated.token,
            ExpireIn = (int)tokenGenerated.expireDate.Subtract(DateTime.UtcNow).TotalSeconds,
            UserName = user.Name,
            FirstName = user.Name.GetFirstName(),
            UserId = user.Id,
            ExpireTimeSpan = tokenGenerated.expireDate,
            ImageUrl = user.ImageUrl
        };

        return response;
    }
    #endregion
}
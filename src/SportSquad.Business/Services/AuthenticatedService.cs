using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using SportSquad.Business.Exceptions;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models;
using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Models.User.Response;

namespace SportSquad.Business.Services;

public class AuthenticatedService : BaseService, IAuthenticatedService
{
    #region Repositories
    private readonly ICreateUserRepository _createUserRepository;
    #endregion

    #region Services
    private readonly ITokenService _tokenService;
    private readonly IEncryptService _encryptService;
    #endregion
    
    #region Validators
    // private readonly LoginRequestValidator _loginRequestValidator;
    #endregion
    
    public AuthenticatedService(
        IMapper mapper, 
        AppSettings appSettings, 
        ResourceManager resourceManager, 
        CultureInfo cultureInfo,
        ICreateUserRepository createUserRepository,
        ITokenService tokenService,
        IEncryptService encryptService
        ) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        // _loginRequestValidator = loginRequestValidator;
        _encryptService = encryptService;
        _tokenService = tokenService;
        _createUserRepository = createUserRepository;
    }

    public async Task<UserSessionResponse> UserAuthenticated(LoginRequest request)
    {
        // var requestValidator = await _loginRequestValidator.ValidateModel(request);
        // if (requestValidator.error) ReturnError<CustomException>(requestValidator.messageError);

        var user = await _createUserRepository.GetByEmail(request.Email);
        if (user is null) ReturnResourceError<CustomException>("USER-INVALID_LOGIN");

        var passwordEncrypt = _encryptService.EncryptPassword(request.Password);
        if (passwordEncrypt != user!.Password) ReturnResourceError<CustomException>("USER-INVALID_LOGIN");

        var tokenGenerated = _tokenService.GenerateToken(user);
        
        return new UserSessionResponse()
        {
            Token = tokenGenerated.token,
            ExpireIn = (int)tokenGenerated.expireDate.Subtract(DateTime.UtcNow).TotalSeconds,
            UserName = user.Name,
            ExpireTymeSpan = tokenGenerated.expireDate
        };
    }
}
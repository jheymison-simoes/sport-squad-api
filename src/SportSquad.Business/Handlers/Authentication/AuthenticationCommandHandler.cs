using System.Globalization;
using System.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SportSquad.Business.Commands.Authentication;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models;
using SportSquad.Business.Models.User.Response;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Handlers.Authentication;

public class AuthenticationCommandHandler : BaseHandler,
    IRequestHandler<LoginCommand, CommandResponse<UserSessionResponse>>
{
    private readonly ICreateUserRepository _createUserRepository;
    
    private readonly IEncryptService _encryptService;
    private readonly ITokenService _tokenService;

    public AuthenticationCommandHandler(
        IMapper mapper, 
        AppSettings appSettings,
        ResourceManager resourceManager, 
        CultureInfo cultureInfo,
        ICreateUserRepository createUserRepository, 
        IEncryptService encryptService,
        ITokenService tokenService) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _createUserRepository = createUserRepository;
        _encryptService = encryptService;
        _tokenService = tokenService;
    }
    
    public async Task<CommandResponse<UserSessionResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _createUserRepository.GetByEmail(request.Email);
        if (user is null) return ReturnError<UserSessionResponse>(ApiResource.USER_INVALID_LOGIN);
        
        var passwordEncrypt = _encryptService.EncryptPassword(request.Password);
        if (passwordEncrypt != user!.Password) return ReturnError<UserSessionResponse>(ApiResource.USER_INVALID_LOGIN);

        var tokenGenerated = _tokenService.GenerateToken(user);
        
        var response = new UserSessionResponse()
        {
            Token = tokenGenerated.token,
            ExpireIn = (int)tokenGenerated.expireDate.Subtract(DateTime.UtcNow).TotalSeconds,
            UserName = user.Name,
            ExpireTymeSpan = tokenGenerated.expireDate
        };
        
        return ReturnReply(response);
    }
}
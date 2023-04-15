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
using SportSquad.Domain.Models;

namespace SportSquad.Business.Services;

public class CreateUserService : BaseService, ICreateUserService
{
    #region Repositories
    private readonly ICreateUserRepository _createUserRepository;
    #endregion

    #region Services
    private readonly IEncryptService _encryptService;
    #endregion

    #region Validators
    private readonly CreateUserRequestValidator _createUserRequestValidator;
    private readonly UserValidator _userValidator;
    private readonly RegisterUserWithGoogleRequestValidator _registerUserWithGoogleRequestValidator;
    #endregion
    
    public CreateUserService(
        IMapper mapper, 
        IOptions<AppSettings> appSettings, 
        ResourceManager resourceManager, 
        CultureInfo cultureInfo,
        ICreateUserRepository createUserRepository,
        IEncryptService encryptService,
        CreateUserRequestValidator createUserRequestValidator, 
        UserValidator userValidator,
        RegisterUserWithGoogleRequestValidator registerUserWithGoogleRequestValidator) 
        : base(mapper, appSettings, resourceManager, cultureInfo)
    {
        _createUserRepository = createUserRepository;
        _createUserRequestValidator = createUserRequestValidator;
        _userValidator = userValidator;
        _registerUserWithGoogleRequestValidator = registerUserWithGoogleRequestValidator;
        _encryptService = encryptService;
    }

    public async Task<UserResponse> CreateUser(CreateUserRequest request)
    {
        var requestValidator = await _createUserRequestValidator.ValidateModel(request);
        if (requestValidator.error) ReturnError<CustomException>(requestValidator.messageError);

        var userDuplicated = await _createUserRepository.IsDuplicated(request.Email);
        if (userDuplicated) ReturnResourceError<CustomException>("USER-EXISTING_USER");
        
        var user = Mapper.Map<User>(request);
        user.Password = _encryptService.EncryptPassword(user.Password);

        var userValidator = await _userValidator.ValidateModel(user);
        if (userValidator.error) ReturnError<CustomException>(userValidator.messageError);
        
        _createUserRepository.Add(user);
        await _createUserRepository.SaveChanges();
        
        return Mapper.Map<UserResponse>(user);
    }
    
    public async Task<UserResponse> CreateUser(CreateUserWithGoogleRequest request)
    {
        var requestValidator = await _registerUserWithGoogleRequestValidator.ValidateModel(request);
        if (requestValidator.error) ReturnError(requestValidator.messageError);

        var userDuplicated = await _createUserRepository.IsDuplicated(request.Email);
        if (userDuplicated) ReturnResourceError("USER-EXISTING_USER");
        
        var user = Mapper.Map<User>(request);
        user.Password = string.Empty;

        var userValidator = await _userValidator.ValidateModel(user);
        if (userValidator.error) ReturnError(userValidator.messageError);
        
        _createUserRepository.Add(user);
        await _createUserRepository.SaveChanges();
        
        return Mapper.Map<UserResponse>(user);
    }
}
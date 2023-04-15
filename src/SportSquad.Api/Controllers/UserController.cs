using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Exceptions;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models;
using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Models.User.Response;

namespace SportSquad.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : BaseController<UserController>
{
    private readonly ICreateUserService _createUserService;
    
    public UserController(
        ILogger<UserController> logger, 
        IMapper mapper,
        ICreateUserService createUserService) : base(logger, mapper)
    {
        _createUserService = createUserService;
    }
    
    [HttpPost]
    public async Task<ActionResult<BaseResponse<UserResponse>>> Create(CreateUserRequest request)
    {
        try
        {
            var result = await _createUserService.CreateUser(request);
            return BaseResponseSuccess(result);
        }
        catch (CustomException cEx)
        {
            return BaseResponseError<UserResponse>(cEx.Message);
        }
        catch (Exception ex)
        {
            return BaseResponseInternalError<UserResponse>(ex.Message);
        }
    }
    
    [HttpPost("CreateWithGoogle")]
    public async Task<ActionResult<BaseResponse<UserResponse>>> CreateWithGoogle(CreateUserWithGoogleRequest request)
    {
        try
        {
            var result = await _createUserService.CreateUser(request);
            return BaseResponseSuccess(result);
        }
        catch (CustomException cEx)
        {
            return BaseResponseError<UserResponse>(cEx.Message);
        }
        catch (Exception ex)
        {
            return BaseResponseInternalError<UserResponse>(ex.Message);
        }
    }
}
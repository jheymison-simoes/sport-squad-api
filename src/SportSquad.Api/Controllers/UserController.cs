using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Commands.User;
using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Models.User.Response;
using SportSquad.Core.Interfaces;

namespace SportSquad.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : BaseController<UserController>
{
    private readonly IMediatorHandler _mediator;
    public UserController(
        ILogger<UserController> logger, 
        IMapper mapper,
        IMediatorHandler mediator) : base(logger, mapper)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var command = Mapper.Map<CreateUserCommand>(request);
        return CustomResponse(await _mediator.SendCommand<CreateUserCommand, UserResponse>(command));
    }

    [HttpPost("CreateWithGoogle")]
    public async Task<IActionResult> CreateWithGoogle([FromBody] CreateUserWithGoogleRequest request)
    {
        var command = Mapper.Map<CreateUserWithGoogleCommand>(request);
        return CustomResponse(await _mediator.SendCommand<CreateUserWithGoogleCommand, UserResponse>(command));
    }
}
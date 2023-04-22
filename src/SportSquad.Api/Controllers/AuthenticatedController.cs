﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Commands.Authentication;
using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Models.User.Response;
using SportSquad.Core.Interfaces;

namespace SportSquad.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthenticatedController : BaseController<AuthenticatedController>
{
    private readonly IMediatorHandler _mediator;
    
    public AuthenticatedController(
        ILogger<AuthenticatedController> logger, 
        IMapper mapper, 
        IMediatorHandler mediator) : base(logger, mapper)
    {
        _mediator = mediator;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var command = Mapper.Map<LoginCommand>(loginRequest);
        return CustomResponse(await _mediator.SendCommand<LoginCommand, UserSessionResponse>(command));
    }
}
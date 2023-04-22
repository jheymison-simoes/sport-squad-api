using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Commands.Squad;
using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Core.Interfaces;

namespace SportSquad.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class SquadController : BaseController<SquadController>
{
    private readonly IMediatorHandler _mediator;
    
    public SquadController(
        ILogger<SquadController> logger,
        IMapper mapper, 
        IMediatorHandler mediator) : base(logger, mapper)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSquadRequest request)
    {
        var command = Mapper.Map<CreateSquadCommand>(request);
        command.UserId = GetUserIdLogged();
        return CustomResponse(await _mediator.SendCommand<CreateSquadCommand, SquadResponse>(command));
    }
}
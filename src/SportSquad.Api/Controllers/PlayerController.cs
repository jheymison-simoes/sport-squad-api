using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Models.Player.Request;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Core.Interfaces;

namespace SportSquad.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PlayerController : BaseController<PlayerController>
{
    public PlayerController(
        ILogger<PlayerController> logger, 
        IMapper mapper,
        IMediatorHandler mediator) : base(logger, mapper, mediator)
    {
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerRequest request)
    {
        var command = Mapper.Map<CreatePlayerCommand>(request);
        command.UserId = GetUserIdLogged();
        return CustomResponse(await Mediator.SendCommand<CreatePlayerCommand, PlayerResponse>(command));
    }
}
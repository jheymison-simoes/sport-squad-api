using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Commands.Squad.Player.PlayerType;
using SportSquad.Business.Models.Player.Request;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Models.PlayerType;
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
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePlayerRequest request)
    {
        var command = Mapper.Map<UpdatePlayerCommand>(request);
        command.UserId = GetUserIdLogged();
        return CustomResponse(await Mediator.SendCommand<UpdatePlayerCommand, PlayerResponse>(command));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeletePlayerCommand(id);
        return CustomResponse(await Mediator.SendCommand<DeletePlayerCommand, PlayerResponse>(command));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? squadId)
    {
        var command = new GetAllPlayerCommand(squadId);
        return CustomResponse(await Mediator.SendCommand<GetAllPlayerCommand, IEnumerable<PlayerResponse>>(command));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var command = new GetPlayerByIdCommand(id);
        return CustomResponse(await Mediator.SendCommand<GetPlayerByIdCommand, PlayerResponse>(command));
    }
    
    [HttpGet("GetAllBySquadId/{squadId:guid}")]
    public async Task<IActionResult> GetAllBySquadId(Guid squadId)
    {
        var command = new GetAllPlayerBySquadIdCommand(squadId);
        return CustomResponse(await Mediator.SendCommand<GetAllPlayerBySquadIdCommand, IEnumerable<PlayerGroupedTypeResponse>>(command));
    }

    #region Player Type
    [HttpGet("PlayerType")]
    public async Task<IActionResult> GetAllPlayerType()
    {
        var command = new GetAllPlayerTypeCommand();
        return CustomResponse(await Mediator.SendCommand<GetAllPlayerTypeCommand, IEnumerable<PlayerTypeResponse>>(command));
    }
    #endregion
}
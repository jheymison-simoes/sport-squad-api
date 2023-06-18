using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Commands.Squad;
using SportSquad.Business.Commands.Squad.SquadConfig;
using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Core.Interfaces;

namespace SportSquad.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SquadController : BaseController<SquadController>
{
    public SquadController(
        ILogger<SquadController> logger,
        IMapper mapper, 
        IMediatorHandler mediator) : base(logger, mapper, mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSquadRequest request)
    {
        var command = Mapper.Map<CreateSquadCommand>(request);
        command.UserId = GetUserIdLogged() ?? Guid.Parse("6ef8a1e2-6572-4e80-be47-34fb991d37e6");
        return CustomResponse(await Mediator.SendCommand<CreateSquadCommand, SquadResponse>(command));
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetAllByUserId(Guid userId)
    {
        var command = new GetAllSquadByUserCommand(userId);
        return CustomResponse(await Mediator.SendCommand<GetAllSquadByUserCommand, IEnumerable<SquadResponse>>(command));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteSquadByIdCommand(id);
        // command.UserId = GetUserIdLogged() ?? Guid.Parse("6ef8a1e2-6572-4e80-be47-34fb991d37e6");
        return CustomResponse(await Mediator.SendCommand<DeleteSquadByIdCommand, SquadResponse>(command));
    }
    
    [HttpPut("SquadConfig")]
    public async Task<IActionResult> UpdateSquadConfig([FromBody] UpdateSquadConfigRequest request)
    {
        var command = Mapper.Map<UpdateSquadConfigCommand>(request);
        return CustomResponse(await Mediator.SendCommand<UpdateSquadConfigCommand, SquadConfigResponse>(command));
    }
    
    [HttpDelete("SquadConfig/{id:guid}")]
    public async Task<IActionResult> DeleteSquadConfig(Guid id)
    {
        var command = new DeleteSquadConfigCommand(id);
        return CustomResponse(await Mediator.SendCommand<DeleteSquadConfigCommand, SquadConfigResponse>(command));
    }
}
﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
[Authorize]
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
        command.UserId = GetUserIdLogged();
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
        return CustomResponse(await Mediator.SendCommand<DeleteSquadByIdCommand, SquadResponse>(command));
    }
    
    [HttpGet("GetTextSquadShare/{squadId:guid}")]
    public async Task<IActionResult> GetTextSquadShare(Guid squadId)
    {
        var command = new GetTextSquadSharedBySquadIdCommand(squadId);
        return CustomResponse(await Mediator.SendCommand<GetTextSquadSharedBySquadIdCommand, string>(command));
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
    
    [HttpPost("AssembleTeams")]
    public async Task<IActionResult> AssemblyTeams([FromBody] AssembleTeamsRequest request)
    {
        var command = Mapper.Map<AssembleTeamsCommand>(request);
        return CustomResponse(await Mediator.SendCommand<AssembleTeamsCommand, List<AssembledTeamResponse>>(command));
    }
    
    [HttpPost("SharedTextAssembledTeams/{squadId:guid}")]
    public async Task<IActionResult> SharedTextAssembledTeams(Guid squadId, [FromBody] List<AssembledTeamResponse> teams)
    {
        var command = new SharedTextAssembledTeamsCommand(squadId, teams);
        return CustomResponse(await Mediator.SendCommand<SharedTextAssembledTeamsCommand, string>(command));
    }
}
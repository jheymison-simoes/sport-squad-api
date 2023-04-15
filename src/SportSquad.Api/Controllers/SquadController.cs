using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Exceptions;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Models.Squad.Response;

namespace SportSquad.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class SquadController : BaseController<SquadController>
{
    private readonly ISquadService _squadService;
    
    public SquadController(
        ILogger<SquadController> logger,
        IMapper mapper, 
        ISquadService squadService) : base(logger, mapper)
    {
        _squadService = squadService;
    }
    
    [HttpPost]
    public async Task<ActionResult<BaseResponse<SquadResponse>>> Create([FromBody] CreateSquadRequest request)
    {
        try
        {
            var userId = GetUserIdLogged();
            request.UserId = userId;

            var result = await _squadService.CreateSquad(request);
            
            return BaseResponseSuccess(result);
        }
        catch (CustomException cEx)
        {
            return BaseResponseError<SquadResponse>(cEx.Message);
        }
        catch (Exception ex)
        {
            return BaseResponseInternalError<SquadResponse>(ex.Message);
        }
    }
}
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Models;
using SportSquad.Core.Command;
using SportSquad.Core.Interfaces;

namespace SportSquad.Api.Controllers;

public abstract class BaseController<TController> : ControllerBase
{
    protected ILogger<TController> Logger;
    protected IMapper Mapper;
    protected IMediatorHandler Mediator;
    private readonly ICollection<string> _errors = new List<string>();

    protected BaseController(
        ILogger<TController> logger,
        IMapper mapper,
        IMediatorHandler mediator)
    {
        Logger = logger;
        Mapper = mapper;
        Mediator = mediator;
    }
    
    protected Guid GetUserIdLogged()
    {
        var id = User.FindFirstValue("Id");
        var userId = !string.IsNullOrWhiteSpace(id) ? Guid.Parse(id) : default;
        return userId;
    }
    
    protected ActionResult CustomResponse<TResponse>(CommandResponse<TResponse> result)
    {
        foreach (var validationFailure in result.ValidationResult?.Errors ?? new())
            AddProcessingError(validationFailure.ErrorMessage);
        
        if (ValidOperation()) return Ok(new CommandBaseResponse<TResponse>(result.Response));
        
        return BadRequest(new CommandBaseResponse<TResponse>(
            result.Response, 
            true, 
            _errors.ToArray()
        ));
    }

    protected bool ValidOperation() => !_errors.Any();

    protected void AddProcessingError(string erro) => _errors.Add(erro);

    protected void ClearProcessingErrors() => this._errors.Clear();
}
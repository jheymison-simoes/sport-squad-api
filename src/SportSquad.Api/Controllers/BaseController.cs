using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportSquad.Business.Models;
using SportSquad.Core.Command;

namespace SportSquad.Api.Controllers;

public abstract class BaseController<TController> : ControllerBase
{
    protected ILogger<TController> Logger;
    protected IMapper Mapper;
    private readonly ICollection<string> _errors = new List<string>();

    protected BaseController(
        ILogger<TController> logger,
        IMapper mapper)
    {
        Logger = logger;
        Mapper = mapper;
    }

    protected ActionResult<BaseResponse<T>> BaseResponseSuccess<T>(T response)
    {
        return StatusCode((int)HttpStatusCode.OK, GenerateBaseResponse(response));
    }
    
    protected ActionResult<BaseResponse<T>> BaseResponseError<T>(string messageError)
    {
        return StatusCode((int)HttpStatusCode.BadRequest, GenerateBaseResponse(messageError));
    }
    
    protected ActionResult<BaseResponse<T>> BaseResponseInternalError<T>(string messageError)
    {
        return StatusCode((int)HttpStatusCode.InternalServerError, GenerateBaseResponse(messageError));
    }

    protected Guid GetUserIdLogged()
    {
        var id = User.FindFirstValue("Id");
        return Guid.Parse(id!);
    }
    
    protected ActionResult CustomResponse<TResponse>(CommandResponse<TResponse> result)
    {
        foreach (var validationFailure in result.ValidationResult?.Errors ?? new())
            AddProcessingError(validationFailure.ErrorMessage);
        
        if (ValidOperation()) return Ok(new CommandBaseResponse<TResponse>(result.Response));
        
        // foreach (var error in _errors) Log.Warning(error);
        
        return BadRequest(new CommandBaseResponse<TResponse>(
            result.Response, 
            true, 
            _errors.ToArray()
        ));
    }

    protected bool ValidOperation() => !_errors.Any();

    protected void AddProcessingError(string erro) => _errors.Add(erro);

    protected void ClearProcessingErrors() => this._errors.Clear();

    #region Private Methods
    private static BaseResponse<T> GenerateBaseResponse<T>(T response)
    {
        return new BaseResponse<T>()
        {
            ContainError = false,
            MessageError = null,
            Response = response
        };
    }
    
    private static BaseResponse<string> GenerateBaseResponse(string messageError)
    {
        return new BaseResponse<string>()
        {
            ContainError = true,
            MessageError = messageError,
            Response = null
        };
    }
    #endregion
}
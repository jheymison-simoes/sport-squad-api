using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using SportSquad.Business.Models;
using SportSquad.Business.Utils;
using SportSquad.Core.Command;

namespace SportSquad.Business.Handlers;

public abstract class BaseHandler : CommandHandler
{
    protected readonly ResourceSet ResourceSet;
    protected readonly IMapper Mapper;
    protected readonly AppSettings AppSettings;

    protected BaseHandler(
        IMapper mapper,
        AppSettings appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo)
    {
        Mapper = mapper;
        AppSettings = appSettings;
        ResourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
    }
    
    protected void AddErrorResource(string message, params object[] parameters)
    {
        message = parameters.Length > default(int) ? message.ResourceFormat(parameters) : message;
        AddError(message);
    }
    
    protected CommandResponse<TResponse> ReturnError<TResponse>(string message, params object[] parameters)
    {
        AddErrorResource(message, parameters);
        return new CommandResponse<TResponse>() { ValidationResult = ValidationResult };
    }
}
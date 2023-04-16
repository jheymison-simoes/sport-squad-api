using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using SportSquad.Business.Models;
using SportSquad.Business.Utils;
using SportSquad.Core.Command;

namespace SportSquad.Business.Handlers;

public class BaseHandler : CommandHandler
{
    protected readonly ResourceSet ResourceSet;
    protected readonly IMapper Mapper;
    protected readonly AppSettings AppSettings;

    protected BaseHandler(
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo)
    {
        Mapper = mapper;
        AppSettings = appSettings.Value;
        ResourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
    }
    
    private void AddErrorResource(string nameResource, params object[] parameters)
    {
        var message = parameters.Length > default(int)
            ? ResourceSet.GetString(nameResource)!.ResourceFormat(parameters)
            : ResourceSet.GetString(nameResource);

        AddError(message);
    }
    
    protected CommandResponse<TResponse> ReturnReplyWithError<TResponse>(string nameResource, params object[] parameters)
    {
        AddErrorResource(nameResource, parameters);
        return new CommandResponse<TResponse>() { ValidationResult = ValidationResult };
    }
}
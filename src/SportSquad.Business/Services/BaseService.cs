using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using SportSquad.Business.Exceptions;
using SportSquad.Business.Models;
using SportSquad.Business.Utils;

namespace SportSquad.Business.Services;

public abstract class BaseService
{
    protected readonly ResourceSet ResourceSet;
    protected readonly IMapper Mapper;
    protected readonly AppSettings AppSettings;

    protected BaseService(
        IMapper mapper,
        IOptions<AppSettings> appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo)
    {
        Mapper = mapper;
        AppSettings = appSettings.Value;
        ResourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
    }

    protected void ReturnResourceError<TException>(string nameResource, params object[] parameters) where TException : Exception
    {
        var mensagem = parameters.Length > default(int)
            ? ResourceSet.GetString(nameResource)!.ResourceFormat(parameters)
            : ResourceSet.GetString(nameResource);
        
        throw (Activator.CreateInstance(typeof(TException), mensagem) as TException)!;
    }
    
    protected void ReturnError<TException>(string message) where TException : Exception
    {
        throw (Activator.CreateInstance(typeof(TException), message) as TException)!;
    }
    
    protected void ReturnResourceError(string nameResource, params object[] parameters)
    {
        var message = parameters.Length > default(int)
            ? ResourceSet.GetString(nameResource)!.ResourceFormat(parameters)
            : ResourceSet.GetString(nameResource);

        throw new Exception(message);
    }
    
    protected void ReturnError(string message) => throw new CustomException(message);
}
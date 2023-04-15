using System.Globalization;
using System.Linq;
using System.Resources;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using SportSquad.Api.Configuration;
using SportSquad.Business.Models;
using SportSquad.Business.Utils;

namespace SportSquad.Business.Tests.Fixture;

public abstract class BaseFixture<TInstance> where TInstance : class
{
    private readonly ResourceSet _resourceSet;
    public readonly AutoMocker Mocker;
    public TInstance Instance;
    protected const string Culture = "pt-BR";
    protected const string CultureFaker = "pt_BR";
    
    protected BaseFixture(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        _resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
        Mocker = new AutoMocker();
        Mocker.Use(new Mock<IOptions<AppSettings>>().Object);
        Instance = Mocker.CreateInstance<TInstance>(true);
    }
    
    public Mock<TResult> GetMocker<TResult>() where TResult : class
    {
        return Mocker.GetMock<TResult>();
    }
    
    public TResult GetMockerObject<TResult>() where TResult : class
    {
        return Mocker.GetMock<TResult>().Object;
    }
    
    public string GetMessageResource(string name, params object[] parameters)
    {
        return parameters.Any()
            ? _resourceSet.GetString(name)!.ResourceFormat(parameters)
            : _resourceSet.GetString(name);
    }

}
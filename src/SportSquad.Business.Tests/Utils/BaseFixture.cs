using System.Globalization;
using System.Resources;
using AutoMapper;
using Moq.AutoMock;
using SportSquad.Api.Configuration;
using SportSquad.Business.Utils;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Tests.Utils;

public abstract class BaseFixture
{
    public ResourceManager ResourceManager;
    public IMapper Mapper;
    public CultureInfo CultureInfo;
    public const string Culture = "pt-BR";
    public const string CultureFaker = "pt_BR";

    protected BaseFixture()
    {
        Mapper = MapperTests.Mapping<AutoMapperConfiguration>();
        ResourceManager = new ResourceManager(typeof(ApiResource));
        CultureInfo = CultureInfo.GetCultureInfo(Culture);
    }

    public abstract void GenerateCommandHandler();

    public string GetMessageResource(string name, params object[] parameters)
    {
        return parameters.Length > 0
            ? ResourceManager.GetString(name.ToString(), CultureInfo.GetCultureInfo(Culture))!.ResourceFormat(parameters)
            : ResourceManager.GetString(name);
    }

    protected AutoMocker CreateAutoMocker()
    {
        var autoMocker = new AutoMocker();
        autoMocker.Use(Mapper);
        autoMocker.Use(ResourceManager);
        autoMocker.Use(CultureInfo);
        return autoMocker;
    }
}
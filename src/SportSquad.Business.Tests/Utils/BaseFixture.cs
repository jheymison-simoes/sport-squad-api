using System.Globalization;
using System.Linq;
using System.Resources;
using AutoMapper;
using Moq.AutoMock;
using SportSquad.Api.Configuration;
using SportSquad.Business.Utils;

namespace SportSquad.Business.Tests.Utils;

public abstract class BaseFixture
{
    public ResourceManager ResourceManager;
    public IMapper Mapper;
    public CultureInfo CultureInfo;
    public const string Culture = "pt-BR";
    public const string CultureFaker = "pt_BR";
    
    public BaseFixture()
    {
        Mapper = Mapper = MapperTests.Mapping<AutoMapperConfiguration>();
        ResourceManager = new ResourceManager(typeof(Api.Resource.ApiResource));
        CultureInfo = CultureInfo.GetCultureInfo(Culture);
    }

    public abstract void GenerateService();

    public string GetMessageResource(string name, params object[] parameters)
    {
        return parameters.Any()
            ? ResourceManager.GetString(name)!.ResourceFormat(parameters)
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
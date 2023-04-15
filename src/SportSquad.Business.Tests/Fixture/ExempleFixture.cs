using System;
using System.Globalization;
using System.Resources;
using AutoMapper;
using SportSquad.Api.Configuration;
using SportSquad.Business.Services;
using SportSquad.Business.Tests.Utils;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(ExempleCollection))]
public class ExempleCollection : ICollectionFixture<ExempleFixture>
{
}

public class ExempleFixture : IDisposable
{
    // public ExempleService ExempleService;
    // public Mock<IExempleRepository> ExempleRepository;
    public ResourceManager ResourceManager;
    public IMapper Mapper;

    public void GenerateExempleService()
    {
        // ExempleRepository = new Mock<IExempleRepository>();
        ResourceManager = new ResourceManager(typeof(Api.Resource.ApiResource));
        Mapper = MapperTests.Mapping<AutoMapperConfiguration>();
        var culture = CultureInfo.GetCultureInfo("pt-BR");
        
        // ExempleService = new ExempleService(
        //     Mapper,
        //     ResourceManager,
        //     culture
        // );
    }
    
    public void Dispose()
    {
    }
}
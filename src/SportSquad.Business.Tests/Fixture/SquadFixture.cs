using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using Bogus;
using SportSquad.Business.Models.Squad.Request;
using SportSquad.Business.Services;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(SquadCollection))]
public class SquadCollection : ICollectionFixture<SquadFixture>
{
}

public class SquadFixture : BaseFixture<SquadService>
{
    public SquadService SquadService;
    
    public SquadFixture() : base(
        new ResourceManager(typeof(Api.Resource.ApiResource)),
        CultureInfo.GetCultureInfo("pt-BR")
        )
    {
        SquadService = Instance;
    }

    public CreateSquadRequest GenerateValidCreateSquadRequest()
    {
        return new Faker<CreateSquadRequest>(CultureFaker)
            .CustomInstantiator(f => new CreateSquadRequest()
            {
                Name = f.Random.String2(50),
                UserId = f.Random.Guid(),
                SquadConfigs = GenerateValidCreateSquadSquadConfigRequest(2)
            });
    }

    #region Private Methods

    private List<CreateSquadSquadConfigRequest> GenerateValidCreateSquadSquadConfigRequest(int quantity)
    {
        return new Faker<CreateSquadSquadConfigRequest>(CultureFaker)
            .CustomInstantiator(f => new CreateSquadSquadConfigRequest()
            {
                AllowSubstitutes = f.Random.Bool(),
                QuantityPlayers = f.Random.Int(10),
                PlayerTypeId = f.Random.Guid()
            }).Generate(quantity);
    }
    

    #endregion
}
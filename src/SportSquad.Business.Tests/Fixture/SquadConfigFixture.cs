using System.Collections.Generic;
using Bogus;
using SportSquad.Domain.Models;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(SquadConfigCollection))]
public class SquadConfigCollection : IClassFixture<SquadConfigFixture>
{
}

public class SquadConfigFixture
{
    private const string CultureFaker = "pt_BR";
    
    public List<SquadConfig> GenerateValidsSquadConfigs(int quantity)
    {
        return new Faker<SquadConfig>(CultureFaker)
            .CustomInstantiator(f => new SquadConfig()
            {
                AllowSubstitutes = f.Random.Bool(),
                PlayerTypeId = f.Random.Guid(),
                SquadId = f.Random.Guid(),
                QuantityPlayers = f.Random.Int(1, 9999)
            }).Generate(quantity);
    }
}
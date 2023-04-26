using System.Collections.Generic;
using Bogus;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Domain.Models;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(SquadCollection))]
public class SquadCollection : IClassFixture<SquadFixture>
{
}

public class SquadFixture
{
    private const string CultureFaker = "pt_BR";
    
    public List<Squad> GenerateValidsSquads(int quantity)
    {
        var playerFixture = new PlayerFixture();
        var squadConfigFixture = new SquadConfigFixture();
        
        return new Faker<Squad>(CultureFaker)
            .CustomInstantiator(f => new Squad()
            {
                Name = f.Random.String2(40),
                UserId = f.Random.Guid(),
                Players = playerFixture.GenerateValidsPlayers(3),
                SquadConfigs = squadConfigFixture.GenerateValidsSquadConfigs(3)
            }).Generate(quantity);
    }
}
using System.Collections.Generic;
using Bogus;
using SportSquad.Domain.Models;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(PlayerCollection))]
public class PlayerCollection : IClassFixture<PlayerFixture>
{
}

public class PlayerFixture
{
    private const string CultureFaker = "pt_BR";
    
    public List<Player> GenerateValidsPlayers(int quantity)
    {
        return new Faker<Player>(CultureFaker)
            .CustomInstantiator(f => new Player(
                f.Name.FullName(),
                f.Random.Guid(),
                f.Random.Guid()
            )).Generate(quantity);
    }
}
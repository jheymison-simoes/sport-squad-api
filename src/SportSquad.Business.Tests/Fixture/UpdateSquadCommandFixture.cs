using Bogus;
using Moq;
using Moq.AutoMock;
using SportSquad.Business.Commands.Squad.SquadConfig;
using SportSquad.Business.Handlers.Squad;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Tests.Utils;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(UpdateSquadCollection))]
public class UpdateSquadCollection : IClassFixture<UpdateSquadCommandFixture>
{
}

public class UpdateSquadCommandFixture : BaseFixture
{
    private AutoMocker _autoMocker;
    public UpdateSquadCommandHandler UpdateSquadCommandHandler;

    #region Repositories
    public Mock<IUpdateSquadRepository> UpdateSquadRepository;
    #endregion
    
    public override void GenerateCommandHandler()
    {
        _autoMocker = CreateAutoMocker();
        UpdateSquadCommandHandler = _autoMocker.CreateInstance<UpdateSquadCommandHandler>();
        UpdateSquadRepository = _autoMocker.GetMock<IUpdateSquadRepository>();
    }

    public UpdateSquadConfigCommand GenerateValidUpdateSquadConfigCommand()
    {
        return new Faker<UpdateSquadConfigCommand>(CultureFaker)
            .CustomInstantiator(f => new UpdateSquadConfigCommand()
            {
                Id = f.Random.Guid(),
                AllowSubstitutes = f.Random.Bool(),
                QuantityPlayers = f.Random.Int(1, 15)
            });
    }
}
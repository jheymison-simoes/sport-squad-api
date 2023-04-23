using Bogus;
using Moq;
using Moq.AutoMock;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Handlers.Player;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Tests.Utils;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(UpdatePlayerCollection))]
public class UpdatePlayerCollection : ICollectionFixture<UpdatePlayerCommandFixture>
{
}

public class UpdatePlayerCommandFixture : BaseFixture
{
    private AutoMocker _mocker;
    public UpdatePlayerCommandHandler UpdatePlayerCommandHandler;

    #region Repositories
    public Mock<IUpdatePlayerRepository> UpdatePlayerRepository;
    #endregion
    
    public override void GenerateCommandHandler()
    {
        _mocker = CreateAutoMocker();
        UpdatePlayerCommandHandler = _mocker.CreateInstance<UpdatePlayerCommandHandler>();
        UpdatePlayerRepository = _mocker.GetMock<IUpdatePlayerRepository>();
    }

    public UpdatePlayerCommand GenerateValidUpdatePlayerCommand()
    {
        return new Faker<UpdatePlayerCommand>(CultureFaker)
            .CustomInstantiator(f => new UpdatePlayerCommand()
            {
                Name = f.Name.FullName(),
                UserId = f.Random.Guid(),
                PlayerTypeId = f.Random.Guid(),
            });
    }
}
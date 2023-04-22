using Bogus;
using Moq;
using Moq.AutoMock;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Handlers.Player;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Tests.Utils;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(CreatePlayerCollection))]
public class CreatePlayerCollection : ICollectionFixture<CreatePlayerCommandFixture>
{
}

public class CreatePlayerCommandFixture : BaseFixture
{
    private AutoMocker _mocker;
    public CreatePlayerCommandHandler CreatePlayerCommandHandler;

    #region Repositories
    public Mock<ICreatePlayerRepository> CreatePlayerRepository;
    #endregion
    
    public override void GenerateCommandHandler()
    {
        _mocker = CreateAutoMocker();
        CreatePlayerCommandHandler = _mocker.CreateInstance<CreatePlayerCommandHandler>();
        CreatePlayerRepository = _mocker.GetMock<ICreatePlayerRepository>();
    }

    public CreatePlayerCommand GenerateValidCreatePlayerCommand()
    {
        return new Faker<CreatePlayerCommand>(CultureFaker)
            .CustomInstantiator(f => new CreatePlayerCommand()
            {
                Name = f.Name.FullName(),
                SquadId = f.Random.Guid(),
                UserId = f.Random.Guid(),
                PlayerTypeId = f.Random.Guid(),
            });
    }
}
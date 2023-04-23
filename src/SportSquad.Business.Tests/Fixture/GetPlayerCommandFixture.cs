using Bogus;
using Moq;
using Moq.AutoMock;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Handlers.Player;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Tests.Utils;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(GetPlayerCollection))]
public class GetPlayerCollection : ICollectionFixture<GetPlayerCommandFixture>
{
}

public class GetPlayerCommandFixture : BaseFixture
{
    private AutoMocker _mocker;
    public GetPlayerCommandHandler GetPlayerCommandHandler;

    #region Repositories
    public Mock<IGetPlayerRepository> GetPlayerRepository;
    #endregion

    public override void GenerateCommandHandler()
    {
        _mocker = CreateAutoMocker();
        GetPlayerCommandHandler = _mocker.CreateInstance<GetPlayerCommandHandler>();
        GetPlayerRepository = _mocker.GetMock<IGetPlayerRepository>();
    }
}
using Moq;
using Moq.AutoMock;
using SportSquad.Business.Handlers.Player;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Tests.Utils;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(DeletePlayerCollection))]
public class DeletePlayerCollection : ICollectionFixture<DeletePlayerCommandFixture>
{
}

public class DeletePlayerCommandFixture : BaseFixture
{
    private AutoMocker _mocker;
    public DeletePlayerCommandHandler DeletePlayerCommandHandler;

    #region Repositories
    public Mock<IDeletePlayerRepository> DeletePlayerRepository;
    #endregion
    
    public override void GenerateCommandHandler()
    {
        _mocker = CreateAutoMocker();
        DeletePlayerCommandHandler = _mocker.CreateInstance<DeletePlayerCommandHandler>();
        DeletePlayerRepository = _mocker.GetMock<IDeletePlayerRepository>();
    }
}
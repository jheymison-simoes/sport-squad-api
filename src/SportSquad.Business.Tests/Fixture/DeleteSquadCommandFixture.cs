using Moq;
using Moq.AutoMock;
using SportSquad.Business.Handlers.Squad;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Tests.Utils;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(DeleteSquadCollection))]
public class DeleteSquadCollection : IClassFixture<DeleteSquadCommandFixture>
{
}

public class DeleteSquadCommandFixture : BaseFixture
{
    private AutoMocker _autoMocker;
    public DeleteSquadCommandHandler DeleteSquadCommandHandler;

    #region Repositories
    public Mock<IDeleteSquadRepository> DeleteSquadRepository;
    #endregion
    
    public override void GenerateCommandHandler()
    {
        _autoMocker = CreateAutoMocker();
        DeleteSquadCommandHandler = _autoMocker.CreateInstance<DeleteSquadCommandHandler>();
        DeleteSquadRepository = _autoMocker.GetMock<IDeleteSquadRepository>();
    }
}
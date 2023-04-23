using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Moq;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Tests.Fixture;
using SportSquad.Business.Utils;
using SportSquad.Core.Resource;
using SportSquad.Domain.Models;
using Xunit;

namespace SportSquad.Business.Tests.Commands;

[Collection(nameof(DeletePlayerCollection))]
public class DeletePlayerCommandHandlerTests : IClassFixture<PlayerFixture>
{
    private readonly DeletePlayerCommandFixture _fixture;
    private readonly PlayerFixture _playerFixture;

    public DeletePlayerCommandHandlerTests(DeletePlayerCommandFixture fixture, PlayerFixture playerFixture)
    {
        _fixture = fixture;
        _playerFixture = playerFixture;
    }

    #region Delete Player
    [Fact(DisplayName = "Do not delete a player if not found by id")]
    [Trait("Handler", "Delete Player Command Handler")]
    [Trait("Method", "Delete Player Command")]
    public async void DeletePlayer_DoNotDelete_PlayerNotFoundById()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = new DeletePlayerCommand(Guid.NewGuid());

        var expected = ApiResource.PLAYER_NOT_FOUND_BY_ID.ResourceFormat(command.Id);

        _fixture.DeletePlayerRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Player)null);

        // Act
        var result = await _fixture.DeletePlayerCommandHandler.Handle(command, new CancellationToken());

        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Delete a player")]
    [Trait("Handler", "Delete Player Command Handler")]
    [Trait("Method", "Delete Player Command")]
    public async void DeletePlayer_Delete_WithSuccess()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = new DeletePlayerCommand(Guid.NewGuid());
        var player = _playerFixture.GenerateValidsPlayers(1).First();
        
        _fixture.DeletePlayerRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(player);

        _fixture.DeletePlayerRepository.Setup(s => s.UnitOfWork.Commit()).ReturnsAsync(true);

        // Act
        var result = await _fixture.DeletePlayerCommandHandler.Handle(command, new CancellationToken());

        // Assert
        result.ValidationResult.Should().BeNull();
        result.Response.Should().NotBeNull();
    }
    #endregion
}
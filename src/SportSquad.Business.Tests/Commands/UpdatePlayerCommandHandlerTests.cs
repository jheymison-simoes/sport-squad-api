using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using Moq;
using SportSquad.Business.Tests.Fixture;
using SportSquad.Business.Utils;
using SportSquad.Core.Resource;
using SportSquad.Domain.Models;
using Xunit;

namespace SportSquad.Business.Tests.Commands;

[Collection(nameof(UpdatePlayerCollection))]
public class UpdatePlayerCommandHandlerTests : IClassFixture<PlayerFixture>
{
    private readonly UpdatePlayerCommandFixture _fixture;
    private readonly PlayerFixture _playerFixture;

    public UpdatePlayerCommandHandlerTests(
        UpdatePlayerCommandFixture fixture,
        PlayerFixture playerFixture)
    {
        _fixture = fixture;
        _playerFixture = playerFixture;
    }

    #region Update Player
    [Fact(DisplayName = "Do not update the player if not found by id")]
    [Trait("Handler", "Update Player Command Handler")]
    [Trait("Method", "Update Player Command")]
    public async void UpdatePlayer_DoNotUpdate_PlayerNotFoundById()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidUpdatePlayerCommand();

        var expected = ApiResource.PLAYER_NOT_FOUND_BY_ID.ResourceFormat(command.Id);

        _fixture.UpdatePlayerRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Player)null);
        
        // Act
        var result = await _fixture.UpdatePlayerCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Do not update the player if there is already a player with the same name")]
    [Trait("Handler", "Update Player Command Handler")]
    [Trait("Method", "Update Player Command")]
    public async void UpdatePlayer_DoNotUpdate_NameIsDuplicated()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidUpdatePlayerCommand();
        var player = _playerFixture.GenerateValidsPlayers(1).First();
        
        var expected = ApiResource.SQUAD_PLAYER_NAME_DUPLICATED.ResourceFormat(command.Id);

        _fixture.UpdatePlayerRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(player);

        _fixture.UpdatePlayerRepository.Setup(s => s.IsDuplicated(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);
        
        // Act
        var result = await _fixture.UpdatePlayerCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Do not update the player if the player type by id is not found")]
    [Trait("Handler", "Update Player Command Handler")]
    [Trait("Method", "Update Player Command")]
    public async void UpdatePlayer_DoNotUpdate_PlayerTypeNotFoundById()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidUpdatePlayerCommand();
        var player = _playerFixture.GenerateValidsPlayers(1).First();
        
        var expected = ApiResource.PLAYER_TYPE_NOT_FOUND_BY_ID.ResourceFormat(command.PlayerTypeId);

        _fixture.UpdatePlayerRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(player);

        _fixture.UpdatePlayerRepository.Setup(s => s.IsDuplicated(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        _fixture.UpdatePlayerRepository.Setup(s => s.ExistsPlayerType(It.IsAny<Guid>()))
            .ReturnsAsync(false);
        
        // Act
        var result = await _fixture.UpdatePlayerCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Update player")]
    [Trait("Handler", "Update Player Command Handler")]
    [Trait("Method", "Update Player Command")]
    public async void UpdatePlayer_Update_WithSuccess()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidUpdatePlayerCommand();
        var player = _playerFixture.GenerateValidsPlayers(1).First();
 
        _fixture.UpdatePlayerRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(player);

        _fixture.UpdatePlayerRepository.Setup(s => s.IsDuplicated(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        _fixture.UpdatePlayerRepository.Setup(s => s.ExistsPlayerType(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        _fixture.UpdatePlayerRepository.Setup(s => s.UnitOfWork.Commit()).ReturnsAsync(true);
        
        // Act
        var result = await _fixture.UpdatePlayerCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Should().BeNull();
        result.Response.Should().NotBeNull();
    }
    #endregion
}
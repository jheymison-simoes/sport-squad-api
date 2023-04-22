using System;
using System.Threading;
using FluentAssertions;
using Moq;
using SportSquad.Business.Tests.Fixture;
using SportSquad.Business.Utils;
using SportSquad.Core.Resource;
using Xunit;

namespace SportSquad.Business.Tests.Commands;

[Collection(nameof(CreatePlayerCollection))]
public class CreatePlayerCommandHandlerTests
{
    private readonly CreatePlayerCommandFixture _fixture;

    public CreatePlayerCommandHandlerTests(CreatePlayerCommandFixture fixture)
    {
        _fixture = fixture;
    }

    #region Create Player
    [Fact(DisplayName = "Do not create a player if one with the same name already exists")]
    [Trait("Handler", "Create Player Command Handler")]
    [Trait("Method", "Create Player Command")]
    public async void CreatePlayer_DoNotCreate_IsDuplicated()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidCreatePlayerCommand();

        var expected = ApiResource.SQUAD_PLAYER_NAME_DUPLICATED;

        _fixture.CreatePlayerRepository.Setup(s => s.IsDuplicatedAync(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);
        
        // Act
        var result = await _fixture.CreatePlayerCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Do not create the player if the squad is not found by id")]
    [Trait("Handler", "Create Player Command Handler")]
    [Trait("Method", "Create Player Command")]
    public async void CreatePlayer_DoNotCreate_SquadNotDoundById()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidCreatePlayerCommand();

        var expected = ApiResource.SQUAD_NOT_FOUND_BY_ID.ResourceFormat(command.SquadId);

        _fixture.CreatePlayerRepository.Setup(s => s.IsDuplicatedAync(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        _fixture.CreatePlayerRepository.Setup(s => s.ExistsSquadAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);
        
        // Act
        var result = await _fixture.CreatePlayerCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Do not create the player if the player type is not found by id")]
    [Trait("Handler", "Create Player Command Handler")]
    [Trait("Method", "Create Player Command")]
    public async void CreatePlayer_DoNotCreate_PlayerTypeNotDoundById()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidCreatePlayerCommand();

        var expected = ApiResource.PLAYER_TYPE_NOT_FOUND_BY_ID.ResourceFormat(command.PlayerTypeId);

        _fixture.CreatePlayerRepository.Setup(s => s.IsDuplicatedAync(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        _fixture.CreatePlayerRepository.Setup(s => s.ExistsSquadAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        
        _fixture.CreatePlayerRepository.Setup(s => s.ExistsPlayerTypeAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);
        
        // Act
        var result = await _fixture.CreatePlayerCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Create player")]
    [Trait("Handler", "Create Player Command Handler")]
    [Trait("Method", "Create Player Command")]
    public async void CreatePlayer_Create_WithSuccess()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidCreatePlayerCommand();
        
        _fixture.CreatePlayerRepository.Setup(s => s.IsDuplicatedAync(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);

        _fixture.CreatePlayerRepository.Setup(s => s.ExistsSquadAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        
        _fixture.CreatePlayerRepository.Setup(s => s.ExistsPlayerTypeAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        _fixture.CreatePlayerRepository.Setup(s => s.UnitOfWork.Commit()).ReturnsAsync(true);
        
        // Act
        var result = await _fixture.CreatePlayerCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Should().BeNull();
        result.Response.Should().NotBeNull();
    }

    #endregion
    
}
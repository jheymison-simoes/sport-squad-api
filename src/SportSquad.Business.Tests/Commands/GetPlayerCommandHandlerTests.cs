using System;
using System.Linq;
using System.Threading;
using Bogus;
using FluentAssertions;
using Moq;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Tests.Fixture;
using SportSquad.Business.Utils;
using SportSquad.Core.Resource;
using SportSquad.Domain.Models;
using Xunit;

namespace SportSquad.Business.Tests.Commands;

[Collection(nameof(GetPlayerCollection))]
public class GetPlayerCommandHandlerTests : IClassFixture<PlayerFixture>
{
    private readonly GetPlayerCommandFixture _fixture;
    private readonly PlayerFixture _playerFixture;

    public GetPlayerCommandHandlerTests(GetPlayerCommandFixture fixture, PlayerFixture playerFixture)
    {
        _fixture = fixture;
        _playerFixture = playerFixture;
    }

    #region Get all players
    [Fact(DisplayName = "Get all players")]
    [Trait("Handler", "Get Player Command Handler")]
    [Trait("Method", "Get Player Command")]
    public async void GetPlayer_Get_GetAllPlayer()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = new GetAllPlayerCommand(null);
        var players = _playerFixture.GenerateValidsPlayersResponse(5);

        _fixture.GetPlayerRepository.Setup(s => s.GetAllAsync(It.IsAny<Guid?>()))
            .ReturnsAsync(players);

        // Act
        var result = await _fixture.GetPlayerCommandHandler.Handle(command, new CancellationToken());

        // Assert
        result.ValidationResult.Should().BeNull();
        result.Response.Should().NotBeNull();
    }
    #endregion

    #region Get player by id
    [Fact(DisplayName = "Do not get the player by id if not found")]
    [Trait("Handler", "Get Player Command Handler")]
    [Trait("Method", "Get Player Command")]
    public async void GetPlayerById_DoNotGet_PlayerNotFoundById()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = new GetPlayerByIdCommand(new Faker().Random.Guid());
        
        var expected = ApiResource.PLAYER_NOT_FOUND_BY_ID.ResourceFormat(command.Id);
        
        _fixture.GetPlayerRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Player)null);

        // Act
        var result = await _fixture.GetPlayerCommandHandler.Handle(command, new CancellationToken());

        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Get player by id")]
    [Trait("Handler", "Get Player Command Handler")]
    [Trait("Method", "Get Player Command")]
    public async void GetPlayerById_Get_WithSuccess()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = new GetPlayerByIdCommand(new Faker().Random.Guid());
        var player = _playerFixture.GenerateValidsPlayers(1).First();
        
        _fixture.GetPlayerRepository.Setup(s => s.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(player);

        // Act
        var result = await _fixture.GetPlayerCommandHandler.Handle(command, new CancellationToken());

        // Assert
        result.ValidationResult.Should().BeNull();
        result.Response.Should().NotBeNull();
    }
    #endregion
}
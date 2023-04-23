using System;
using System.Threading;
using FluentAssertions;
using Moq;
using SportSquad.Business.Commands.Squad.Player;
using SportSquad.Business.Tests.Fixture;
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

        _fixture.GetPlayerRepository.Setup(s => s.GetAll(It.IsAny<Guid?>()))
            .ReturnsAsync(players);

        // Act
        var result = await _fixture.GetPlayerCommandHandler.Handle(command, new CancellationToken());

        // Assert
        result.ValidationResult.Should().BeNull();
        result.Response.Should().NotBeNull();
    }
    #endregion
}
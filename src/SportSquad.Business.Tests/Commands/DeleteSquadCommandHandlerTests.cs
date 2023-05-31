using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using Moq;
using SportSquad.Business.Commands.Squad.SquadConfig;
using SportSquad.Business.Tests.Fixture;
using SportSquad.Business.Utils;
using SportSquad.Core.Resource;
using SportSquad.Domain.Models;
using Xunit;

namespace SportSquad.Business.Tests.Commands;

[Collection(nameof(DeleteSquadCollection))]
public class DeleteSquadCommandHandlerTests
{
    private readonly DeleteSquadCommandFixture _fixture;

    public DeleteSquadCommandHandlerTests(DeleteSquadCommandFixture fixture)
    {
        _fixture = fixture;
    }

    #region Delete squad config
    [Fact(DisplayName = "Do not delete a squad configuration if not found by id")]
    [Trait("Handler", "Delete Squad Command Handler")]
    [Trait("Method", "Delete Squad Command")]
    public async void DeleteSquadConfig_DoNotDelete_SquadConfigNotFoundById()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = new DeleteSquadConfigCommand(Guid.NewGuid());

        var expected = ApiResource.SQUAD_CONFIG_NOT_FOUND_BY_ID.ResourceFormat(command.Id);

        _fixture.DeleteSquadRepository.Setup(s => s.GetSquadConfigByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((SquadConfig)null);
        
        // Act
        var result = await _fixture.DeleteSquadCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Delete squad configuration with success")]
    [Trait("Handler", "Delete Squad Command Handler")]
    [Trait("Method", "Delete Squad Command")]
    public async void DeleteSquadConfig_Delete_WithSucess()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = new DeleteSquadConfigCommand(Guid.NewGuid());

        _fixture.DeleteSquadRepository.Setup(s => s.GetSquadConfigByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new SquadConfig());

        _fixture.DeleteSquadRepository.Setup(s => s.GetAllSquadPlayersAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<Player>());

        _fixture.DeleteSquadRepository.Setup(s => s.DeleteSquadConfig(It.IsAny<SquadConfig>()));

        _fixture.DeleteSquadRepository.Setup(s => s.UnitOfWork.Commit()).ReturnsAsync(true);
        
        // Act
        var result = await _fixture.DeleteSquadCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Should().BeNull();
        result.Response.Should().NotBeNull();
    }

    #endregion
}
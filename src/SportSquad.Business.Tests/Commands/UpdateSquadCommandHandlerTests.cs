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

[Collection(nameof(UpdateSquadCollection))]
public class UpdateSquadCommandHandlerTests : IClassFixture<SquadConfigFixture>
{
    private readonly UpdateSquadCommandFixture _fixture;
    private readonly SquadConfigFixture _squadConfigFixture;

    public UpdateSquadCommandHandlerTests(UpdateSquadCommandFixture fixture, SquadConfigFixture squadConfigFixture)
    {
        _fixture = fixture;
        _squadConfigFixture = squadConfigFixture;
    }

    #region Update squad config
    [Fact(DisplayName = "Do not update a squad configuration if not found by id")]
    [Trait("Handler", "Update Squad Command Handler")]
    [Trait("Method", "Update Squad Command")]
    public async void UpdateSquad_DoNotUpdate_SquadConfigNotFoundById()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidUpdateSquadConfigCommand();

        var expected = ApiResource.SQUAD_CONFIG_NOT_FOUND_BY_ID.ResourceFormat(command.Id);

        _fixture.UpdateSquadRepository.Setup(s => s.GetSquadConfigByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((SquadConfig)null);
        
        // Act
        var result = await _fixture.UpdateSquadCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Errors.Should().Contain(x => x.ErrorMessage == expected);
    }
    
    [Fact(DisplayName = "Update squad configuration successfully")]
    [Trait("Handler", "Update Squad Command Handler")]
    [Trait("Method", "Update Squad Command")]
    public async void UpdateSquad_Update_WithSuccess()
    {
        // Arrange
        _fixture.GenerateCommandHandler();

        var command = _fixture.GenerateValidUpdateSquadConfigCommand();
        var squadConfig = _squadConfigFixture.GenerateValidsSquadConfigs(1).First();
        
        _fixture.UpdateSquadRepository.Setup(s => s.GetSquadConfigByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(squadConfig);

        _fixture.UpdateSquadRepository.Setup(s => s.UpdateSquadConfig(It.IsAny<SquadConfig>()));

        _fixture.UpdateSquadRepository.Setup(s => s.UnitOfWork.Commit()).ReturnsAsync(true);
        
        // Act
        var result = await _fixture.UpdateSquadCommandHandler.Handle(command, new CancellationToken());
        
        // Assert
        result.ValidationResult.Should().BeNull();
        result.Response.Should().NotBeNull();
    }
    #endregion
}
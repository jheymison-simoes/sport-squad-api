using System.Resources;
using FluentAssertions;
using SportSquad.Business.Exceptions;
using SportSquad.Business.Tests.Fixture;
using SportSquad.Business.Tests.Utils;
using Xunit;

namespace SportSquad.Business.Tests.Services;

// [Collection(nameof(SquadCollection))]
// public class SquadServiceTests
// {
//     private readonly SquadFixture _fixture;
//
//     public SquadServiceTests(SquadFixture fixture)
//     {
//         _fixture = fixture;
//     }
//     
//     [Fact(DisplayName = "Do not create squad if any property is invalid")]
//     [Trait("Category", "Authenticated Service")]
//     public void UserAuthenticated_NotAuthenticated_IfAnyPropertyIsInvalid()
//     {
//         //Arrange
//         var request = _fixture.GenerateValidCreateSquadRequest();
//         request.Name = string.Empty;
//         
//         var expected = _fixture.GetMessageResource("SQUAD-NAME_EMPT");
//         
//         //Act
//         var result = Assert.ThrowsAsync<CustomException>(
//             () => _fixture.SquadService.CreateSquad(request)
//         );
//
//         //Assert
//         result.Result.Message.Should().Be(expected);
//     }
// }
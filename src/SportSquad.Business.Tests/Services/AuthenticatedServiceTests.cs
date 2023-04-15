using System.Linq;
using Bogus;
using FluentAssertions;
using Moq;
using SportSquad.Business.Exceptions;
using SportSquad.Business.Tests.Fixture;
using SportSquad.Domain.Models;
using Xunit;

namespace SportSquad.Business.Tests.Services;

[Collection(nameof(AuthenticatedCollection))]
public class AuthenticatedServiceTests : IClassFixture<UserFixture>
{
    private readonly AuthenticatedFixture _fixture;
    private readonly UserFixture _userFixture;

    public AuthenticatedServiceTests(
        AuthenticatedFixture fixture, 
        UserFixture userFixture)
    {
        _fixture = fixture;
        _userFixture = userFixture;
    }
    
    [Fact(DisplayName = "Do not authenticate user if any property is invalid")]
    [Trait("Category", "Authenticated Service")]
    public void UserAuthenticated_NotAuthenticated_IfAnyPropertyIsInvalid()
    {
        //Arrange
        _fixture.GenerateService();

        var loginRequest = _fixture.GenerateValidLoginRequest();
        loginRequest.PhoneNumber = null;

        var expected = _fixture.GetMessageResource("LOGIN-REQUEST-PHONE_NUMER_EMPTY");
        
        //Act
        var result = Assert.ThrowsAsync<CustomException>(
            () => _fixture.AuthenticatedService.UserAuthenticated(loginRequest)
        );

        //Assert
        result.Result.Message.Should().Be(expected);
    }
    
    [Fact(DisplayName = "Don't authenticate the user if you can't find him over the phone")]
    [Trait("Category", "Authenticated Service")]
    public void UserAuthenticated_NotAuthenticated_UserNotFoundByPhoneNumber()
    {
        //Arrange
        _fixture.GenerateService();

        var loginRequest = _fixture.GenerateValidLoginRequest();

        var expected = _fixture.GetMessageResource("USER-INVALID_LOGIN", loginRequest.PhoneNumber);

        _fixture.UserRepository.Setup(s =>
            s.GetByPhoneNumber(It.IsAny<string>(), It.IsAny<string>())
        ).ReturnsAsync((User)null);
        
        //Act
        var result = Assert.ThrowsAsync<CustomException>(
            () => _fixture.AuthenticatedService.UserAuthenticated(loginRequest)
        );

        //Assert
        result.Result.Message.Should().Be(expected);
    }
    
    [Fact(DisplayName = "Don't authenticate user if password can't be hashed")]
    [Trait("Category", "Authenticated Service")]
    public void UserAuthenticated_NotAuthenticated_ErrorGeneratingPasswordHash()
    {
        //Arrange
        _fixture.GenerateService();

        var loginRequest = _fixture.GenerateValidLoginRequest();
        var user = _userFixture.GenerateValidUsers(1).First();
        
        var expected = _fixture.GetMessageResource("USER-INVALID_LOGIN");

        _fixture.UserRepository.Setup(s =>
            s.GetByPhoneNumber(It.IsAny<string>(), It.IsAny<string>())
        ).ReturnsAsync(user);
        
        _fixture.EncryptService.Setup(s =>
            s.EncryptPassword(It.IsAny<string>())
        ).Throws(new CustomException(expected));
        
        //Act
        var result = Assert.ThrowsAsync<CustomException>(
            () => _fixture.AuthenticatedService.UserAuthenticated(loginRequest)
        );

        //Assert
        result.Result.Message.Should().Be(expected);
    }
    
    [Fact(DisplayName = "Authenticate user")]
    [Trait("Category", "Authenticated Service")]
    public async void UserAuthenticated_Authenticated_WithSuccess()
    {
        //Arrange
        _fixture.GenerateService();

        var loginRequest = _fixture.GenerateValidLoginRequest();
        var user = _userFixture.GenerateValidUsers(1).First();
        var hash = new Faker().Random.Hash();
        user.Password = hash;
        
        _fixture.UserRepository.Setup(s =>
            s.GetByPhoneNumber(It.IsAny<string>(), It.IsAny<string>())
        ).ReturnsAsync(user);
        
        _fixture.EncryptService.Setup(s =>
            s.EncryptPassword(It.IsAny<string>())
        ).Returns(hash);
        
        _fixture.TokenService.Setup(s =>
            s.GenerateToken(It.IsAny<User>())
        ).Returns((new Faker().Random.String2(150), new Faker().Date.Recent()));
        
        //Act
        var result = await _fixture.AuthenticatedService.UserAuthenticated(loginRequest);

        //Assert
        result.Should().NotBeNull();
    }
}
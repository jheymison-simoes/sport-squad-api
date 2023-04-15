using FluentAssertions;
using SportSquad.Business.Exceptions;
using SportSquad.Business.Tests.Fixture;
using Xunit;

namespace SportSquad.Business.Tests.Services;

[Collection(nameof(UserCollection))]
public class CreateUserServiceTests
{
    private readonly UserFixture _fixture;

    public CreateUserServiceTests(UserFixture fixture)
    {
        _fixture = fixture;
    }

    #region Create user with google
    [Fact(DisplayName = "Do not create the user if the request is invalid")]
    [Trait("Category", "Create User Service")]
    public async void CreateUserWithGoogle_NotCreate_InvalidRequest()
    {
        //Arrange
        _fixture.GenerateService();
        
        var request = _fixture.GenerateCreateUserWithGoogleRequestValid();
        request.Email = string.Empty;
        
        var expected = _fixture.GetMessageResource("LOGIN-REQUEST-EMAIL_EMPTY");
        
        //Act
        var result = await Assert.ThrowsAsync<CustomException>(
            () => _fixture.CreateUserService.CreateUser(request)
        );
        
        //Assert
        result.Message.Should().Contain(expected);
    }
    

    #endregion
}
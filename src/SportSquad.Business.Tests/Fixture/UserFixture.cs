using System.Collections.Generic;
using Bogus;
using Moq;
using Moq.AutoMock;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Services;
using SportSquad.Business.Tests.Utils;
using SportSquad.Domain.Models;
using Xunit;

namespace SportSquad.Business.Tests.Fixture;

[CollectionDefinition(nameof(UserCollection))]
public class UserCollection : ICollectionFixture<UserFixture>
{
}

public class UserFixture : BaseFixture
{
    public AutoMocker Mocker;
    public CreateUserService CreateUserService;

    #region Repositories
    public Mock<ICreateUserRepository> CreateUserRepository;
    #endregion

    #region Services
    public Mock<IEncryptService> EncryptService;
    #endregion

    #region Validators
    public Mock<CreateUserRequestValidator> CreateUserRequestValidator;
    public Mock<UserValidator> UserValidator;
    public Mock<RegisterUserWithGoogleRequestValidator> RegisterUserWithGoogleRequestValidator;
    #endregion
    
    public override void GenerateService()
    {
        Mocker = CreateAutoMocker();
        Mocker.Use(new CreateUserRequestValidator(ResourceManager, CultureInfo));
        Mocker.Use(new UserValidator(ResourceManager, CultureInfo));
        Mocker.Use(new RegisterUserWithGoogleRequestValidator(ResourceManager, CultureInfo));
        CreateUserService = Mocker.CreateInstance<CreateUserService>();
        CreateUserRepository = Mocker.GetMock<ICreateUserRepository>();
        EncryptService = Mocker.GetMock<IEncryptService>();
    }
    
    public List<User> GenerateValidUsers(int quantidade)
    {
        return new Faker<User>(CultureFaker)
            .CustomInstantiator(f =>
            {
                var firstName = new Faker().Name.FirstName();
                var lastName = new Faker().Name.LastName();
                var fullName = $"{firstName} {lastName}";
                
                return new User(
                    fullName,
                    f.Random.Replace("##"),
                    f.Random.Replace("#########"),
                    f.Internet.Email(firstName, lastName),
                    f.Random.String2(8)
                );
            }).Generate(quantidade);
    }

    public CreateUserWithGoogleRequest GenerateCreateUserWithGoogleRequestValid()
    {
        return FakeDataGenerator.GenerateFakeData<CreateUserWithGoogleRequest>();
    }
}
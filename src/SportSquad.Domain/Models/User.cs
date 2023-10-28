using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Domain.Validate;

namespace SportSquad.Domain.Models;

public class User : Entity
{
    public string Name { get; set; }
    public string Ddd { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ImageUrl { get; set; }

    #region RelacionShip
    public List<Squad> Squads { get; set; }
    public List<Player> Players { get; set; }
    #endregion

    public User()
    {
    }
    
    public User(
        string name, 
        string ddd, 
        string phoneNumber, 
        string email, 
        string password)
    {
        Name = name;
        Ddd = ddd;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
    }
    
    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }
}

public class UserValidator : BaseDomainAbstractValidator<User>
{
    public UserValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {

        RuleFor(r => r.Name)
            .NotEmpty()
                .WithMessage(GetMessageResource("USER-NAME_EMPTY"))
            .Must(r => r.Length is > 3 and <= 150)
                .WithMessage(GetMessageResource("USER-NAME_INVALID_NUMBER_CHARACTERS", 3, 150));
        
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage(GetMessageResource("USER-EMAIL_EMPTY"))
            .EmailAddress()
            .WithMessage(GetMessageResource("USER-EMAIL_INVALID"));
    }
}

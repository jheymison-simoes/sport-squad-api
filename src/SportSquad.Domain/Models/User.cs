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

        // TODO - Verificar se vai continuar esta validação quando implementar login com google
        // RuleFor(r => r.Ddd)
        //     .NotEmpty()
        //         .WithMessage(GetMessageResource("USER-DDD_EMPTY"))
        //     .Length(2)
        //         .WithMessage(GetMessageResource("USER-DDD_INVALID_NUMBER_CHARACTERS", 2));
        //
        // RuleFor(r => r.PhoneNumber)
        //     .NotEmpty()
        //         .WithMessage(GetMessageResource("USER-PHONE_NUMBER_EMPTY"))
        //     .Length(9)
        //         .WithMessage(GetMessageResource("USER-PHONE_NUMBER_INVALID_NUMBER_CHARACTERS", 9));
        //
        // When(r => !string.IsNullOrWhiteSpace(r.Email), () =>
        // {
        //     RuleFor(r => r.Email)
        //         .NotEmpty()
        //             .WithMessage(GetMessageResource("USER-EMAIL_EMPTY"))
        //         .EmailAddress()
        //             .WithMessage(GetMessageResource("USER-EMAIL_INVALID"));
        // });
        
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage(GetMessageResource("USER-EMAIL_EMPTY"))
            .EmailAddress()
            .WithMessage(GetMessageResource("USER-EMAIL_INVALID"));
        
        // RuleFor(r => r.Password)
        //     .NotEmpty()
        //         .WithMessage(GetMessageResource("USER-PASSWORD_EMPTY"));
    }
}

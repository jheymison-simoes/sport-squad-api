using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Validator;

namespace SportSquad.Business.Models.User.Request;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Ddd { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}

public class CreateUserRequestValidator : BaseBusinessAbastractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator(
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

        RuleFor(r => r.Ddd)
            .NotEmpty()
            .WithMessage(GetMessageResource("USER-DDD_EMPTY"))
            .Length(2)
            .WithMessage(GetMessageResource("USER-DDD_INVALID_NUMBER_CHARACTERS", 2));
        
        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .WithMessage(GetMessageResource("USER-PHONE_NUMBER_EMPTY"))
            .Length(9)
            .WithMessage(GetMessageResource("USER-PHONE_NUMBER_INVALID_NUMBER_CHARACTERS", 9));
        
        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage(GetMessageResource("USER-PASSWORD_EMPTY"));
    }
}
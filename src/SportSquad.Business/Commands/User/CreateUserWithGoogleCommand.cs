using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.User.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;

namespace SportSquad.Business.Commands.User;

public class CreateUserWithGoogleCommand : Command<UserResponse>
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class CreateUserWithGoogleValidator : BaseBusinessAbastractValidator<CreateUserWithGoogleCommand>
{
    public CreateUserWithGoogleValidator(
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
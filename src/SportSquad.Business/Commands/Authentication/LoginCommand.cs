using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.User.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;

namespace SportSquad.Business.Commands.Authentication;

public class LoginCommand : Command<UserSessionResponse>
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginCommandValidator : BaseBusinessAbastractValidator<LoginCommand>
{
    public LoginCommandValidator(ResourceManager resourceManager, CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage(GetMessageResource("LOGIN-REQUEST-EMAIL_EMPTY"))
            .EmailAddress()
            .WithMessage(GetMessageResource("LOGIN-REQUEST-INVALID_EMAIL"));
            
        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage(GetMessageResource("LOGIN-REQUEST-PASSWORD_EMPTY"));
    }
}
using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Validator;

namespace SportSquad.Business.Models.User.Request;

public class LoginRequest
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginRequestValidator : BaseBusinessAbastractValidator<LoginRequest>
{
    public LoginRequestValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
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
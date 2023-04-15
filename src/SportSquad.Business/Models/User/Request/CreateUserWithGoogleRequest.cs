using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Validator;

namespace SportSquad.Business.Models.User.Request;

public class CreateUserWithGoogleRequest
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

public class RegisterUserWithGoogleRequestValidator : BaseBusinessAbastractValidator<CreateUserWithGoogleRequest>
{
    public RegisterUserWithGoogleRequestValidator(
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.Email)
            .NotEmpty()
                .WithMessage(GetMessageResource("LOGIN-REQUEST-EMAIL_EMPTY"))
            .EmailAddress()
                .WithMessage(GetMessageResource("LOGIN-REQUEST-INVALID_EMAIL"));
        
        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
                .WithMessage(GetMessageResource("LOGIN-REQUEST-PHONE_NUMBER_EMPTY"))
            .Length(11)
                .WithMessage(GetMessageResource("LOGIN-REQUEST-PHONE_NUMBER_INVALID", 11));
    }
}
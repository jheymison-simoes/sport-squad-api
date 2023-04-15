using System.Globalization;
using System.Resources;
using System.Text.Json.Serialization;
using FluentValidation;
using SportSquad.Business.Validator;

namespace SportSquad.Business.Models.Squad.Request;

public class CreateSquadRequest
{
    public string Name { get; set; }
    [JsonIgnore]
    public Guid UserId { get; set; }
    public List<CreateSquadSquadConfigRequest> SquadConfigs { get; set; }
}

public class CreateSquadValidator : BaseBusinessAbastractValidator<CreateSquadRequest>
{
    public CreateSquadValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.Name)
            .NotEmpty()
                .WithMessage(GetMessageResource("SQUAD-NAME_EMPTY"))
            .Must(r => r.Length is > 3 and <= 150)
                .WithMessage(GetMessageResource("SQUAD-INVALID_NUMBER_CHARACTERS", 3, 150));
        
        RuleFor(r => r.UserId)
            .NotEmpty()
                .WithMessage(GetMessageResource("SQUAD-USER_ID_EMPTY"));

        RuleFor(r => r.SquadConfigs)
            .NotEmpty()
                .WithMessage(GetMessageResource("CREATE-SQUAD-SQUAD_CONFIGS_EMPTY"));;
        
        RuleForEach(r => r.SquadConfigs)
            .SetValidator(new CreateSquadSquadConfigValidator(resourceManager, cultureInfo));
    }
}
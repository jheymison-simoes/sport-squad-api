using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Commands.Squad.SquadConfig;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;

namespace SportSquad.Business.Commands.Squad;

public class CreateSquadCommand : Command<SquadResponse>
{
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public List<CreateSquadConfigCommand> SquadConfigs { get; set; }
}

public class CreateSquadValidator : BaseBusinessAbastractValidator<CreateSquadCommand>
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
            .SetValidator(new CreateSquadConfigValidator(resourceManager, cultureInfo));
    }
}
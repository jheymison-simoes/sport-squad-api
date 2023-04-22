using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Validator;

namespace SportSquad.Business.Commands.Squad.SquadConfig;

public class CreateSquadConfigCommand
{
    public int QuantityPlayers { get; set; }
    public Guid PlayerTypeId { get; set; }
    public bool AllowSubstitutes { get; set; }
}

public class CreateSquadConfigValidator : BaseBusinessAbastractValidator<CreateSquadConfigCommand>
{
    public CreateSquadConfigValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.QuantityPlayers)
            .NotEmpty()
            .WithMessage(GetMessageResource("SQUAD-CONFIG-QUANTITY_PLAYERS_EMPTY"));
        
        RuleFor(r => r.PlayerTypeId)
            .NotEmpty()
            .WithMessage(GetMessageResource("SQUAD-CONFIG-PLAYER_TYPE_ID_EMPTY"));
    }
}
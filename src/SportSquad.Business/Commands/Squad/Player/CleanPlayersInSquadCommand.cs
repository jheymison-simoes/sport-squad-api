using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad.Player;

public class CleanPlayersInSquadCommand :  Command<bool>
{
    public Guid SquadId { get; set; }
    
    public CleanPlayersInSquadCommand(Guid squadId)
    {
        SquadId = squadId;
    }
}

public class CleanPlayersInSquadValidator : BaseBusinessAbastractValidator<CleanPlayersInSquadCommand>
{
    public CleanPlayersInSquadValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo
    ) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.SquadId)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_ID_EMPTY);
    }
}
using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad.Player;

public class GetAllPlayerCommand : Command<IEnumerable<PlayerResponse>>
{
    public Guid? SquadId { get; set; }
    
    public GetAllPlayerCommand(Guid? squadId)
    {
        SquadId = squadId;
    }
}

public class GetAllPlayerValidator : BaseBusinessAbastractValidator<GetAllPlayerCommand>
{
    public GetAllPlayerValidator(
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        When(r => r.SquadId.HasValue, () =>
        {
            RuleFor(r => r.SquadId)
                .NotEmpty()
                .WithMessage(ApiResource.SQUAD_ID_EMPTY);
        });
    }
}
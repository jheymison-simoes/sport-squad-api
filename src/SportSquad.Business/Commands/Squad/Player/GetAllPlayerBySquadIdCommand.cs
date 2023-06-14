using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad.Player;

public class GetAllPlayerBySquadIdCommand : Command<List<PlayerGroupedTypeResponse>>
{
    public Guid SquadId { get; set; }
    
    public GetAllPlayerBySquadIdCommand(Guid squadId)
    {
        SquadId = squadId;
    }
}

public class GetAllPlayerBySquadIdValidator : BaseBusinessAbastractValidator<GetAllPlayerBySquadIdCommand>
{
    public GetAllPlayerBySquadIdValidator(
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.SquadId)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_ID_EMPTY);
    }
}
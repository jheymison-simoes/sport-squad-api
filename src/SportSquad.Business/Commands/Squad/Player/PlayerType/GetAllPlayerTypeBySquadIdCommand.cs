using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.PlayerType;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad.Player.PlayerType;

public class GetAllPlayerTypeBySquadIdCommand : Command<List<PlayerTypeResponse>>
{
    public Guid SquadId { get; set; }
    
    public GetAllPlayerTypeBySquadIdCommand(Guid squadId)
    {
        SquadId = squadId;
    }
}

public class GetAllPlayerTypeBySquadIdValidator : BaseBusinessAbastractValidator<GetAllPlayerTypeBySquadIdCommand>
{
    public GetAllPlayerTypeBySquadIdValidator(ResourceManager resourceManager, CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.SquadId)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_ID_EMPTY);
    }
}
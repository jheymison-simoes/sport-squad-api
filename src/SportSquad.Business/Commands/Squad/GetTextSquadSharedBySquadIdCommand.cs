using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad;

public class GetTextSquadSharedBySquadIdCommand : Command<string>
{
    public Guid SquadId { get; set; }
    
    public GetTextSquadSharedBySquadIdCommand(Guid squadId)
    {
        SquadId = squadId;
    }
}

public class GetTextSquadSharedBySquadIdCommandValidator : BaseBusinessAbastractValidator<GetTextSquadSharedBySquadIdCommand>
{
    public GetTextSquadSharedBySquadIdCommandValidator(
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.SquadId)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_ID_EMPTY);
    }
}
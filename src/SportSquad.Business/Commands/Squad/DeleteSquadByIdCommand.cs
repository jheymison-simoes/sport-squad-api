using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad;

public class DeleteSquadByIdCommand : Command<SquadResponse>
{
    public Guid SquadId { get; set; }
    
    public DeleteSquadByIdCommand(Guid squadId)
    {
        SquadId = squadId;
    }
}

public class DeleteSquadByIdCommandValidator : BaseBusinessAbastractValidator<DeleteSquadByIdCommand>
{
    public DeleteSquadByIdCommandValidator(
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.SquadId)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_ID_EMPTY);
    }
}
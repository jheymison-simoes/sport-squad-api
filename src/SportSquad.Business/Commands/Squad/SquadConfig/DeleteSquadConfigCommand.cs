using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad.SquadConfig;

public class DeleteSquadConfigCommand : Command<SquadConfigResponse>
{
    public Guid Id { get; set; }
    
    public DeleteSquadConfigCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteSquadConfigValidator : BaseBusinessAbastractValidator<DeleteSquadConfigCommand>
{
    public DeleteSquadConfigValidator(
        ResourceManager resourceManager,
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_CONFIG_ID_EMPTY);
    }
}
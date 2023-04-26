using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad.SquadConfig;

public class UpdateSquadConfigCommand : Command<SquadConfigResponse>
{
    public Guid Id { get; set; }
    public int QuantityPlayers { get; set; }
    public bool AllowSubstitutes { get; set; }
}

public class UpdateSquadConfigValidator : BaseBusinessAbastractValidator<UpdateSquadConfigCommand>
{
    public UpdateSquadConfigValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_CONFIG_ID_EMPTY);
        
        RuleFor(r => r.QuantityPlayers)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_CONFIG_QUANTITY_PLAYERS_EMPTY);
    }
}
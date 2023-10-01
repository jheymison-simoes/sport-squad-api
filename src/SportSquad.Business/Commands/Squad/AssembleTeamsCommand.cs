using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad;

public class AssembleTeamsCommand : Command<List<AssembledTeamResponse>>
{
    public Guid SquadId { get; set; }
    public int QuantityTeams { get; set; }
    public bool Balanced { get; set; }
    
    public AssembleTeamsCommand(Guid squadId, int quantityTeams, bool balanced)
    {
        SquadId = squadId;
        QuantityTeams = quantityTeams;
        Balanced = balanced;
    }
}

public class AssembleTeamValidator : BaseBusinessAbastractValidator<AssembleTeamsCommand>
{
    public AssembleTeamValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.SquadId)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_ID_EMPTY);
            
        RuleFor(r => r.QuantityTeams)
            .NotEmpty()
            .WithMessage(ApiResource.ASSEMBLED_TEAM_QUANTITY_TEAM_EMPTY);
    }
}
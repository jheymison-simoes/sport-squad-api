using System.Globalization;
using System.Resources;
using FluentValidation;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Validator;
using SportSquad.Core.Command;
using SportSquad.Core.Resource;

namespace SportSquad.Business.Commands.Squad;

public class SharedTextAssembledTeamsCommand : Command<string>
{
    public Guid SquadId { get; set; }
    public List<AssembledTeamResponse> AssembledTeams { get; set; }
    
    public SharedTextAssembledTeamsCommand(Guid squadId, List<AssembledTeamResponse> assembledTeams)
    {
        SquadId = squadId;
        AssembledTeams = assembledTeams;
    }
}

public class SharedTextAssembledTeamsValidator : BaseBusinessAbastractValidator<SharedTextAssembledTeamsCommand>
{
    public SharedTextAssembledTeamsValidator(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(resourceManager, cultureInfo)
    {
        RuleFor(r => r.SquadId)
            .NotEmpty()
            .WithMessage(ApiResource.SQUAD_ID_EMPTY);
        
        RuleFor(r => r.AssembledTeams)
            .NotEmpty()
            .WithMessage(ApiResource.LIST_ASSEMBLED_TEAMS_EMPTY);
    }
}
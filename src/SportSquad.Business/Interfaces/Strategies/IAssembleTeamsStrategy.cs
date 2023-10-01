using SportSquad.Business.Models.Squad.Response;

namespace SportSquad.Business.Interfaces.Strategies;

public interface IAssembleTeamsStrategy
{
    List<AssembledTeamResponse> Assemble(int quantityTeams, List<TeamResponse> players);
}
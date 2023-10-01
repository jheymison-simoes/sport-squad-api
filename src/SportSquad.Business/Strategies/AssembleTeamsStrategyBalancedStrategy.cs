using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using SportSquad.Business.Interfaces.Strategies;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Services;

namespace SportSquad.Business.Strategies;

public class AssembleTeamsStrategyBalancedStrategy : BaseService, IAssembleTeamsStrategyBalancedStrategy
{
    public AssembleTeamsStrategyBalancedStrategy(
        IMapper mapper,
        IOptions<AppSettings> appSettings, 
        ResourceManager resourceManager,
        CultureInfo cultureInfo
    ) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
    }
    
    public List<AssembledTeamResponse> Assemble(int quantityTeams, List<TeamResponse> players)
    {
        if (!players.Any() || quantityTeams == default) return new();
        if (quantityTeams > players.Count) throw ReturnError("A quantidade de times é maior que a quantidade de jogadores linhas");
                
        var playersOrdered = OrderBySkillLevel(players);

        var teams = CreateTeams(quantityTeams, playersOrdered);

        var teamsDivided = SplitTeams(quantityTeams, playersOrdered, teams);
        return teamsDivided;
    }

    #region Private Methods
    private List<TeamResponse> OrderBySkillLevel(List<TeamResponse> players)
        => players.OrderByDescending(p => p.SkillLevel).ThenBy(p => p.PlayerName).ToList();
    
    private List<AssembledTeamResponse> CreateTeams(int quantityTeams, List<TeamResponse> players)
    {
        var response = new List<AssembledTeamResponse>();
        for (var i = 0; i < quantityTeams; i++)
        {
            response.Add(new AssembledTeamResponse()
            {
                TeamName = $"Time {i + 1}",
                SquadId = Guid.NewGuid(),
                Players = new()
            });
        }
        return response;
    }

    private List<AssembledTeamResponse> SplitTeams(int quantityTeams, List<TeamResponse> players, List<AssembledTeamResponse> teams)
    {
        var currentTeam = 0;
        foreach (var player in players)
        {
            teams[currentTeam].Players.Add(player);
            currentTeam = (currentTeam + 1) % quantityTeams;
        }

        return teams;
    }
    #endregion
}
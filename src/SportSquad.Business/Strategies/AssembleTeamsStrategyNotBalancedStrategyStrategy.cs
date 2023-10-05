using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using SportSquad.Business.Interfaces.Strategies;
using SportSquad.Business.Models;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Business.Services;
using SportSquad.Domain.Enumerators;

namespace SportSquad.Business.Strategies;

public class AssembleTeamsStrategyNotBalancedStrategyStrategy : BaseService, IAssembleTeamsStrategyNotBalancedStrategy
{
    public AssembleTeamsStrategyNotBalancedStrategyStrategy(
        IMapper mapper,
        AppSettings appSettings,
        ResourceManager resourceManager,
        CultureInfo cultureInfo
    ) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
    }
    
    public List<AssembledTeamResponse> Assemble(int quantityTeams, List<TeamResponse> players)
    {
        if (!players.Any() || quantityTeams == default) return new();
        if (quantityTeams > players.Count) throw ReturnError("A quantidade de times é maior que a quantidade de jogadores linhas");
                
        var playersShuffled = Shuffle(players);
        var teams = CreateTeams(quantityTeams, playersShuffled);
        
        var teamsDivided = SplitTeams(quantityTeams, playersShuffled, teams);
        return teamsDivided;
    }

    #region Private Methods
    private List<TeamResponse> Shuffle(List<TeamResponse> teams)
    {
        var random = new Random();
        var n = teams.Count;
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            (teams[k], teams[n]) = (teams[n], teams[k]);
        }

        return teams;
    }

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
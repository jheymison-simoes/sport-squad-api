namespace SportSquad.Business.Models.Squad.Response;

public class AssembledTeamResponse
{
    public Guid SquadId { get; set; }
    public string TeamName { get; set; }
    public List<TeamResponse> Players { get; set; }
}

public class TeamResponse
{
    public Guid PlayerId { get; set; }
    public string PlayerName { get; set; }
    public Guid PlayerTypeId { get; set; }
    public int PlayerTypeCode { get; set; }
    public string PlayerTypeName { get; set; }
    public int SkillLevel { get; set; }
}
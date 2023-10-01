namespace SportSquad.Business.Models.Squad.Request;

public class AssembleTeamsRequest
{
    public Guid SquadId { get; set; }
    public int QuantityTeams { get; set; }
    public bool Balanced { get; set; }
}
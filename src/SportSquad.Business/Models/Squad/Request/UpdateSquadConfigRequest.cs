namespace SportSquad.Business.Models.Squad.Request;

public class UpdateSquadConfigRequest
{
    public Guid Id { get; set; }
    public int QuantityPlayers { get; set; }
    public bool AllowSubstitutes { get; set; }
}
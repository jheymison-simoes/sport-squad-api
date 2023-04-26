namespace SportSquad.Business.Models.Squad.Response;

public class SquadConfigResponse
{
    public Guid Id { get; set; }
    public int QuantityPlayers { get; set; }
    public bool AllowSubstitutes { get; set; }
    public Guid SquadId { get; set; }
    public Guid PlayerTypeId { get; set; }
}
namespace SportSquad.Business.Models.Player.Response;

public class PlayerGroupedTypeResponse
{
    public Guid PlayerTypeId { get; set; }
    public string PlayerTypeName { get; set; }
    public string PlayerTypeIcon { get; set; }
    public int QuantityMaxPlayers { get; set; }
    public int QuantityPlayers { get; set; }
    public bool AllowSubstitutes { get; set; }
    public List<PlayerGroupedPlayerResponse> Players { get; set; }
}

public class PlayerGroupedPlayerResponse
{
    public Guid PlayerId { get; set; }
    public string Name { get; set; }
    public int SkillLevel { get; set; }
    public bool Substitute { get; set; }
    public int Index { get; set; }
}
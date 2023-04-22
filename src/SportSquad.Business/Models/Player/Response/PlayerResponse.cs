using SportSquad.Business.Models.PlayerType;

namespace SportSquad.Business.Models.Player.Response;

public class PlayerResponse
{
    public string Name { get; set; }
    public Guid PlayerTypeId { get; set; }
    public Guid SquadId { get; set; }
    public Guid? UserId { get; set; }
    public PlayerTypeResponse PlayerType { get; set; }
}
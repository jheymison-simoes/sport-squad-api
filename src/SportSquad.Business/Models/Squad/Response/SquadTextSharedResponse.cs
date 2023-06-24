using SportSquad.Business.Models.Player.Response;

namespace SportSquad.Business.Models.Squad.Response;

public class SquadTextSharedResponse
{
    public Guid SquadId { get; set; }
    public string SquadName { get; set; }
    public List<PlayerGroupedTypeResponse> PlayersType { get; set; }
}
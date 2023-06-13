namespace SportSquad.Business.Models.Player.Request;

public class CreatePlayerRequest
{
    public string Name { get; set; }
    public Guid PlayerTypeId { get; set; }
    public Guid SquadId { get; set; }
    public int SkillLevel { get; set; }
}
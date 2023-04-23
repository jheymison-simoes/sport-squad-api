namespace SportSquad.Business.Models.Player.Request;

public class UpdatePlayerRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid PlayerTypeId { get; set; }
    public Guid? UserId { get; set; }
}
namespace SportSquad.Business.Models.Squad.Response;

public class SquadResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }
}
namespace SportSquad.Business.Models.PlayerType;

public class PlayerTypeResponse
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
}
namespace SportSquad.Business.Models.Squad.Request;

public class UpdateSquadRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<UpdateSquadConfigRequest> SquadConfigs { get; set; }
}
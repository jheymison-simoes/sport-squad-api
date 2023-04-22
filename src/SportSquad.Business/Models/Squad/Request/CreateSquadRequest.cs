namespace SportSquad.Business.Models.Squad.Request;

public class CreateSquadRequest
{
    public string Name { get; set; }
    public List<CreateSquadConfigRequest> SquadConfigs { get; set; }
}

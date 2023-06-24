namespace SportSquad.Business.Models;

public class AppSettings
{
    public string SecretToken { get; set; }
    public string GoogleClientId { get; set; }
    public string GoogleClientSecret { get; set; }
    public string KissLogOrganizationId {get; set;}
    public string KissLogApplicationId {get; set;}
    public string KissLogApiUrl {get; set;}
    public string SportSquadAppUrl { get; set; }
}
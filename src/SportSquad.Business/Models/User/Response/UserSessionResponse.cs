namespace SportSquad.Business.Models.User.Response;

public class UserSessionResponse
{
    public string UserName { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public int ExpireIn { get; set; }
    public DateTime ExpireTymeSpan { get; set; }
}
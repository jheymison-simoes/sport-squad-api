namespace SportSquad.Business.Models.User.Response;

public class UserSessionResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public int ExpireIn { get; set; }
    public DateTime ExpireTimeSpan { get; set; }
    public string ImageUrl { get; set; }
}
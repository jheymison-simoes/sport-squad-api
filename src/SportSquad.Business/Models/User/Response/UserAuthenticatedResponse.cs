namespace SportSquad.Business.Models.User.Response;

public class UserAuthenticatedResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
    public string Ddd { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string AcessToken { get; set; }
}
namespace SportSquad.Business.Models.User.Request;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Ddd { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}
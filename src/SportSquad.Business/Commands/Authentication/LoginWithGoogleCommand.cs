using SportSquad.Business.Models.User.Response;
using SportSquad.Core.Command;

namespace SportSquad.Business.Commands.Authentication;

public class LoginWithGoogleCommand : Command<UserSessionResponse>
{
    public string Provider { get; set; }
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AuthToken { get; set; }
    public string IdToken { get; set; }
    public string AuthorizationCode { get; set; }
    public object Response { get; set; }
}
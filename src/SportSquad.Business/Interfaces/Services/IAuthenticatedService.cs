using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Models.User.Response;

namespace SportSquad.Business.Interfaces.Services;

public interface IAuthenticatedService
{
    Task<UserSessionResponse> UserAuthenticated(LoginRequest request);
}
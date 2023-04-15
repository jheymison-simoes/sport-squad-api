using SportSquad.Business.Models.User.Request;
using SportSquad.Business.Models.User.Response;

namespace SportSquad.Business.Interfaces.Services;

public interface ICreateUserService
{
    Task<UserResponse> CreateUser(CreateUserRequest request);
    Task<UserResponse> CreateUser(CreateUserWithGoogleRequest request);
}
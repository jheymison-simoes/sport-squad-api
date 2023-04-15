using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Services;

public interface ITokenService
{
    (string token, DateTime expireDate) GenerateToken(User user);
}
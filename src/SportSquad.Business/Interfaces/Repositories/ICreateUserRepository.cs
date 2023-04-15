using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface ICreateUserRepository : IBaseRepository<User>
{
    Task<User> GetByPhoneNumber(string ddd, string phoneNumber);
    Task<User> GetByEmail(string email);
    Task<bool> IsDuplicated(string email);
}
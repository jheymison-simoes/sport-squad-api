using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IUpdatePlayerRepository : IBaseRepository<Player>
{
    Task<bool> IsDuplicated(string name, Guid squadId);
    Task<bool> ExistsPlayerType(Guid playerTypeId);
}
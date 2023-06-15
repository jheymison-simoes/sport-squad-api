using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IUpdatePlayerRepository : IBaseRepository<Player>
{
    Task<bool> IsDuplicated(string name, Guid squadId);
    Task<bool> ExistsPlayerType(Guid playerTypeId);
    Task<SquadConfig> GetSquadConfigBySquadIdAsync(Guid squadId, Guid playerTypeId);
    Task<int> GetQuantityPlayersSquadAsync(Guid squadId, Guid playerTypeId);
}
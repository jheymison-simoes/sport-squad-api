using SportSquad.Business.Models.PlayerType;
using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IGetPlayerTypeRepository : IBaseRepository<PlayerType>
{
    Task<bool> ExistSquadAsync(Guid squadId);
    Task<List<PlayerTypeResponse>> GetAllBySquadIdAsync(Guid squadId);
}
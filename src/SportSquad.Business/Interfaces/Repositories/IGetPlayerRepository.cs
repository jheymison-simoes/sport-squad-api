using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Models.PlayerType;
using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IGetPlayerRepository : IBaseRepository<Player>
{
    Task<IEnumerable<PlayerResponse>> GetAllAsync(Guid? squadId);
    Task<IEnumerable<PlayerTypeResponse>> GetAllPlayersTypesAsync();
    Task<IEnumerable<PlayerGroupedTypeResponse>> GetAllBySquadIdGroupedAsync(Guid squadId);
}
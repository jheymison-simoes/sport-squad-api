using SportSquad.Business.Models.Player.Response;
using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IGetPlayerRepository : IBaseRepository<Player>
{
    Task<IEnumerable<PlayerResponse>> GetAllAsync(Guid? squadId);
}
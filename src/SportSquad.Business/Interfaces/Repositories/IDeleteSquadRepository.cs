using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IDeleteSquadRepository : IBaseRepository<Squad>
{
    Task<SquadConfig> GetSquadConfigByIdAsync(Guid id);
    void DeleteSquadConfig(SquadConfig squadConfig);
    Task<List<Player>> GetAllSquadPlayersAsync(Guid squadId, Guid playerTypeId);
    void DeletePlayers(IEnumerable<Player> players);
}
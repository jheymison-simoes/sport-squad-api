using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IDeleteSquadRepository : IBaseRepository<Squad>
{
    Task<SquadConfig> GetSquadConfigByIdAsync(Guid id);
    void DeleteSquadConfig(SquadConfig squadConfig);
    void DeleteSquadConfig(List<SquadConfig> squadConfigs);
    Task<List<Player>> GetAllSquadPlayersAsync(Guid squadId, Guid playerTypeId);
    void DeletePlayers(IEnumerable<Player> players);
    Task<Squad> GetByIdWithPlayersAsync(Guid requestSquadId);
}
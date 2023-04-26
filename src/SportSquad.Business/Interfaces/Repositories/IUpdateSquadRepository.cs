using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IUpdateSquadRepository : IBaseRepository<Squad>
{
    Task<SquadConfig> GetSquadConfigByIdAsync(Guid id);
    void UpdateSquadConfig(SquadConfig squadConfig);
}
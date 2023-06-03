using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IGetSquadRepository : IBaseRepository<Squad>
{
    Task<IEnumerable<Squad>> GetAllByUserIdAsync(Guid userId);
}
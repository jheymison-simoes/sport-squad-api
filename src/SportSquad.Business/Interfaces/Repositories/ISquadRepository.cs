using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface ISquadRepository : IBaseRepository<Squad>
{
    Task<bool> IsDuplicated(string name, Guid userId);
}
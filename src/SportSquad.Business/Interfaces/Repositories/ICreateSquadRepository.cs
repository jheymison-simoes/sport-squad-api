using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface ICreateSquadRepository : IBaseRepository<Squad>
{
    Task<bool> IsDuplicated(string name, Guid userId);
    Task<bool> UserExists(Guid userId);
    Task<bool> PlayerTypeExists(Guid playerTypeId);
}
using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface ICreatePlayerRepository : IBaseRepository<Player>
{
    Task<bool> IsDuplicatedAync(string name, Guid squadId);
    Task<bool> ExistsSquadAsync(Guid squadId);
    Task<bool> ExistsPlayerTypeAsync(Guid playerTypeId);
}
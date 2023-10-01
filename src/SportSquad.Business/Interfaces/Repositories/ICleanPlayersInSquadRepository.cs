using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface ICleanPlayersInSquadRepository  : IBaseRepository<Squad>
{
    Task RemoveAllPlayerAsync(Guid squadId);
}
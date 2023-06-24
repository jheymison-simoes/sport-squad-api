using SportSquad.Business.Models.Squad.Response;
using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IGetSquadTextSaredRepository : IBaseRepository<Squad>
{
    Task<SquadTextSharedResponse> GetSquadByIdWithPlayersAsync(Guid squadId);
}
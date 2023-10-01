using SportSquad.Business.Models.Squad.Response;
using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IAssembleTeamsRepository : IBaseRepository<Squad>
{
    Task<List<TeamResponse>> GetPlayersBySquadIdAsync(Guid squadId);
}
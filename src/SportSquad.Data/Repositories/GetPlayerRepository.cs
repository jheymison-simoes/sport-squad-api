using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Models.PlayerType;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class GetPlayerRepository : BaseRepository<Player>, IGetPlayerRepository
{
    public GetPlayerRepository(SqlContext db) : base(db)
    {
    }

    public async Task<IEnumerable<PlayerResponse>> GetAllAsync(Guid? squadId)
    {
        return await DbSet.AsNoTracking()
            .Where(p => (!squadId.HasValue || p.SquadId == squadId))
            .Select(p => new PlayerResponse()
            {
                PlayerType = new PlayerTypeResponse()
                {
                    Name = p.PlayerType.Name
                },
                Name = p.Name,
                PlayerTypeId = p.PlayerTypeId,
                SquadId = p.SquadId,
                UserId = p.UserId
            }).ToListAsync();
    }
}
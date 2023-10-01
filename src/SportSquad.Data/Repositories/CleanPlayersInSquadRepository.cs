using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class CleanPlayersInSquadRepository : BaseRepository<Squad>, ICleanPlayersInSquadRepository
{
    public CleanPlayersInSquadRepository(SqlContext db) : base(db)
    {
    }

    public async Task RemoveAllPlayerAsync(Guid squadId)
    {
        var players = await Db.Players.AsNoTracking()
            .Where(p => p.SquadId == squadId)
            .ToListAsync();

        if (!players.Any()) return;
        
        Db.RemoveRange(players);
        await Db.SaveChangesAsync();
    }
}
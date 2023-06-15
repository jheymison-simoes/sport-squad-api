using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class UpdatePlayerRepository : BaseRepository<Player>, IUpdatePlayerRepository
{
    public UpdatePlayerRepository(SqlContext db) : base(db)
    {
    }

    public async Task<bool> IsDuplicated(string name, Guid squadId)
    {
        return await DbSet.AsNoTracking()
            .AnyAsync(p => p.Name == name && p.SquadId == squadId);
    }

    public async Task<bool> ExistsPlayerType(Guid playerTypeId)
    {
        return await Db.PlayerTypes.AsNoTracking()
            .AnyAsync(pt => pt.Id == playerTypeId);
    }
    
    public async Task<SquadConfig> GetSquadConfigBySquadIdAsync(Guid squadId, Guid playerTypeId)
    {
        return await Db.SquadConfigs.AsNoTracking()
            .FirstOrDefaultAsync(sc => sc.SquadId == squadId && sc.PlayerTypeId == playerTypeId);
    }
    
    public async Task<int> GetQuantityPlayersSquadAsync(Guid squadId, Guid playerTypeId)
    {
        return await Db.Players.AsNoTracking()
            .CountAsync(p => p.SquadId == squadId && p.PlayerTypeId == playerTypeId);
    }
}
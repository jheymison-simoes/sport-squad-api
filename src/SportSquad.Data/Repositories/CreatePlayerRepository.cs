using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class CreatePlayerRepository : BaseRepository<Player>, ICreatePlayerRepository
{
    public CreatePlayerRepository(SqlContext db) : base(db)
    {
    }

    public async Task<bool> IsDuplicatedAync(string name, Guid squadId)
    {
        return await DbSet.AsNoTracking()
            .AnyAsync(p => p.Name == name && p.SquadId == squadId);
    }

    public async Task<bool> ExistsSquadAsync(Guid squadId)
    {
        return await Db.Squads.AsNoTracking()
            .AnyAsync(s => s.Id == squadId);
    }

    public async Task<bool> ExistsPlayerTypeAsync(Guid playerTypeId)
    {
        return await Db.PlayerTypes.AsNoTracking()
            .AnyAsync(s => s.Id == playerTypeId);
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

    public async Task SavePlayerAsync(Player player)
    {
        Db.Players.Add(player);
        await Db.SaveChangesAsync();
        await Db.Entry(player).Reference(s => s.PlayerType).LoadAsync();
    }
}
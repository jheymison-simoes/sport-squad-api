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
}
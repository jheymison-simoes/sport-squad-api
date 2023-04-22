using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class CreateSquadRepository : BaseRepository<Squad>, ICreateSquadRepository
{
    public CreateSquadRepository(SqlContext db) : base(db)
    {
    }

    public async Task<bool> IsDuplicated(string name, Guid userId)
    {
        return await DbSet.AnyAsync(s => s.Name == name && s.UserId == userId);
    }

    public async Task<bool> UserExists(Guid userId)
    {
        return await Db.Users.AsNoTracking()
            .AnyAsync(u => u.Id == userId);
    }

    public async Task<bool> PlayerTypeExists(Guid playerTypeId)
    {
        return await Db.PlayerTypes.AsNoTracking()
            .AnyAsync(pt => pt.Id == playerTypeId);
    }
}
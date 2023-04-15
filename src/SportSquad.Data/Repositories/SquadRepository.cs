using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class SquadRepository : BaseRepository<Squad>, ISquadRepository
{
    public SquadRepository(SqlContext db) : base(db)
    {
    }

    public async Task<bool> IsDuplicated(string name, Guid userId)
    {
        return await DbSet.AnyAsync(s => s.Name == name && s.UserId == userId);
    }
}
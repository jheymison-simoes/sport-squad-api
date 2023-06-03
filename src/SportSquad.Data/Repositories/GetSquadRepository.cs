using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class GetSquadRepository : BaseRepository<Squad>, IGetSquadRepository
{
    public GetSquadRepository(SqlContext db) : base(db)
    {
    }

    public async Task<IEnumerable<Squad>> GetAllByUserIdAsync(Guid userId)
    {
        var result = await DbSet.AsNoTracking()
            .Where(s => s.UserId == userId)
            .ToListAsync();

        return result;
    }
}
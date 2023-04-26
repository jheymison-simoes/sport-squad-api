using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class UpdateSquadRepository : BaseRepository<Squad>, IUpdateSquadRepository
{
    public UpdateSquadRepository(SqlContext db) : base(db)
    {
    }

    public async Task<SquadConfig> GetSquadConfigByIdAsync(Guid id)
    {
        var result = await Db.SquadConfigs.AsNoTracking()
            .FirstOrDefaultAsync(sc => sc.Id == id);
        return result;
    }

    public void UpdateSquadConfig(SquadConfig squadConfig)
    {
        Db.SquadConfigs.Update(squadConfig);
    }
}
using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models.PlayerType;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class GetPlayerTypeRepository : BaseRepository<PlayerType>, IGetPlayerTypeRepository
{
    public GetPlayerTypeRepository(SqlContext db) : base(db)
    {
    }

    public async Task<bool> ExistSquadAsync(Guid squadId)
    {
        var result = await Db.Squads.AsNoTracking()
            .AnyAsync(s => s.Id == squadId);

        return result;
    }

    public async Task<List<PlayerTypeResponse>> GetAllBySquadIdAsync(Guid squadId)
    {
        var result = await Db.SquadConfigs.AsNoTracking()
            .Where(sc => sc.SquadId == squadId)
            .Select(sc => new PlayerTypeResponse
            {
                Code = sc.PlayerType.Code,
                CreatedAt = sc.CreatedAt,
                Name = sc.PlayerType.Name,
                Icon = sc.PlayerType.Icon,
                Id = sc.PlayerTypeId
            }).ToListAsync();

        return result;
    }
}
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Models.PlayerType;
using SportSquad.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<PlayerTypeResponse>> GetAllPlayersTypesAsync()
    {
        return await Db.PlayerTypes.AsNoTracking()
            .Select(pt => new PlayerTypeResponse()
            {
                Name = pt.Name,
                CreatedAt = pt.CreatedAt,
                Code = pt.Code,
                Id = pt.Id,
                Icon = pt.Icon
            }).ToListAsync();
    }

    public async Task<IEnumerable<PlayerGroupedTypeResponse>> GetAllBySquadIdGroupedAsync(Guid squadId)
    {
        var allPlayers = await Db.SquadConfigs.AsNoTracking()
            .Where(sc => sc.SquadId == squadId)
            .OrderBy(sc => sc.CreatedAt)
            .Select(sc => new PlayerGroupedTypeResponse()
            {
                PlayerTypeId = sc.PlayerTypeId,
                PlayerTypeIcon = sc.PlayerType.Icon,
                PlayerTypeName = sc.PlayerType.Name,
                QuantityMaxPlayers = sc.QuantityPlayers,
                QuantityPlayers = sc.Squad.Players.Count(p => p.PlayerTypeId == sc.PlayerTypeId),
                Players = sc.Squad.Players.Where(p => p.PlayerTypeId == sc.PlayerTypeId)
                    .OrderBy(p=> p.CreatedAt)
                    .Select(p => new PlayerGroupedPlayerResponse()
                    {
                        Name = p.Name,
                        PlayerId = p.Id,
                        SkillLevel = p.SkillLevel
                    }).ToList()
            }).ToListAsync();
        
        return allPlayers;
    }
}
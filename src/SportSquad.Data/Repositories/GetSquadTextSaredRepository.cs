using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models.Player.Response;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class GetSquadTextSaredRepository : BaseRepository<Squad>, IGetSquadTextSaredRepository
{
    public GetSquadTextSaredRepository(SqlContext db) : base(db)
    {
    }
    
    public async Task<SquadTextSharedResponse> GetSquadByIdWithPlayersAsync(Guid squadId)
    {
        var allPlayers = await Db.Squads.AsNoTracking()
            .Where(s => s.Id == squadId)
            .Select(s => new SquadTextSharedResponse()
            {
                SquadId = s.Id,
                SquadName = s.Name,
                PlayersType = s.SquadConfigs.OrderBy(sc => sc.CreatedAt)
                    .Select(sc => new PlayerGroupedTypeResponse()
                    {
                        PlayerTypeId = sc.PlayerTypeId,
                        PlayerTypeIcon = sc.PlayerType.Icon,
                        PlayerTypeName = sc.PlayerType.Name,
                        QuantityMaxPlayers = sc.QuantityPlayers,
                        QuantityPlayers = s.Players.Count(p => p.PlayerTypeId == sc.PlayerTypeId),
                        AllowSubstitutes = sc.AllowSubstitutes,
                        Players = s.Players.Where(p => p.PlayerTypeId == sc.PlayerTypeId)
                            .OrderBy(p => p.CreatedAt)
                            .Select(p => new PlayerGroupedPlayerResponse()
                            {
                                Name = p.Name,
                                PlayerId = p.Id,
                                SkillLevel = p.SkillLevel,
                            }).ToList()
                    }
                ).ToList()
            }).FirstOrDefaultAsync();
        
        return allPlayers;
    }
}
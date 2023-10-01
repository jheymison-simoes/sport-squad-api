using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Business.Models.Squad.Response;
using SportSquad.Domain.Enumerators;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class AssembleTeamsRepository : BaseRepository<Squad>, IAssembleTeamsRepository
{
    public AssembleTeamsRepository(SqlContext db) : base(db)
    {
    }

    public async Task<List<TeamResponse>> GetPlayersBySquadIdAsync(Guid squadId)
    {
        var quantityMaxPlayers = (await Db.SquadConfigs.AsNoTracking()
                .FirstOrDefaultAsync(sc => 
                    sc.SquadId == squadId && 
                    sc.PlayerType.Code == (int)PlayerTypeEnum.Line
                )
            )?.QuantityPlayers ?? default(int);
        
        return await Db.Players.AsNoTracking()
            .Where(p => p.SquadId == squadId && p.PlayerType.Code != (int)PlayerTypeEnum.GoalPeeker)
            .OrderBy(p => p.CreatedAt)
            .Take(quantityMaxPlayers)
            .Select(p => new TeamResponse()
            {
                PlayerId = p.Id,
                PlayerName = p.Name,
                SkillLevel = p.SkillLevel,
                PlayerTypeId = p.PlayerTypeId,
                PlayerTypeCode = p.PlayerType.Code,
                PlayerTypeName = p.PlayerType.Name
            }).ToListAsync();
    }
}
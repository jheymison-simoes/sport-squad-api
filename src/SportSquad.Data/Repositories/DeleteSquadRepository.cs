using Microsoft.EntityFrameworkCore;
using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class DeleteSquadRepository : BaseRepository<Squad>, IDeleteSquadRepository
{
    public DeleteSquadRepository(SqlContext db) : base(db)
    {
    }

    public async Task<SquadConfig> GetSquadConfigByIdAsync(Guid id)
    {
        var result = await Db.SquadConfigs.AsNoTracking()
            .FirstOrDefaultAsync(sc => sc.Id == id);
        return result;
    }

    public void DeleteSquadConfig(SquadConfig squadConfig) => Db.SquadConfigs.Remove(squadConfig);
    

    public async Task<List<Player>> GetAllSquadPlayersAsync(Guid squadId, Guid playerTypeId)
    {
        var result = await Db.Players.AsNoTracking()
            .Where(p => p.SquadId == squadId && p.PlayerTypeId == playerTypeId)
            .ToListAsync();
        
        return result;
    }

    public void DeletePlayers(IEnumerable<Player> players) => Db.Players.RemoveRange(players);
}
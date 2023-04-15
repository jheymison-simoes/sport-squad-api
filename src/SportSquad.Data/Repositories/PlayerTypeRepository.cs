using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class PlayerTypeRepository : BaseRepository<PlayerType>, IPlayerTypeRepository
{
    public PlayerTypeRepository(SqlContext db) : base(db)
    {
    }
}
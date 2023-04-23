using SportSquad.Business.Interfaces.Repositories;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public class DeletePlayerRepository : BaseRepository<Player>, IDeletePlayerRepository
{
    public DeletePlayerRepository(SqlContext db) : base(db)
    {
    }
}
using SportSquad.Core.Interfaces;

namespace SportSquad.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SqlContext _context;

    public UnitOfWork(SqlContext context)
    {
        _context = context;
    }

    public async Task<bool> Commit()
    {
        var rowsEffecteds = await _context.SaveChangesAsync();
        return rowsEffecteds > 1;
    }
}
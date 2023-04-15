using SportSquad.Business.Interfaces.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SportSquad.Domain.Models;

namespace SportSquad.Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : Entity
{
    private bool _isDisposed;
    protected readonly SqlContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(SqlContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }
    
    #region Default
    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }
    
    public void Add(IEnumerable<TEntity> entities)
    {
        DbSet.AddRange(entities);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }
    
    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }
    
    public void Remove(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }
    
    public async Task<ICollection<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
    {
        var query = DbSet.AsQueryable<TEntity>();
        return await query.AsNoTracking().Where(predicate).ToListAsync();
    }
    
    public async Task<ICollection<TEntity>> Search(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, Entity>>[] includes)
    {
        var query = DbSet.AsQueryable<TEntity>();
        includes.ToList().ForEach(i => query = query.AsNoTracking().Include(i));
        return await query.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return (await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id))!;
    }
    
    public async Task<TEntity> GetById(Guid id, params Expression<Func<TEntity, Entity>>[] includes)
    {
        var query = DbSet.AsQueryable<TEntity>();
        includes.ToList().ForEach(i => query = query.AsNoTracking().Include(i));
        return (await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public async Task<ICollection<TEntity>> GetAll()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }
    
    public async Task<ICollection<TEntity>> GetAll(params Expression<Func<TEntity, Entity>>[] includes)
    {
        var query = DbSet.AsQueryable<TEntity>();
        includes.ToList().ForEach(i => query = query.AsNoTracking().Include(i));
        return await query.AsNoTracking().ToListAsync();
    }
    
    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
    {
        return (await DbSet.AsNoTracking()
            .Where(predicate)
            .FirstOrDefaultAsync())!;
    }
    
    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, Entity>>[] includes)
    {
        var query = DbSet.AsQueryable<TEntity>();
        includes.ToList().ForEach(i => query = query.AsNoTracking().Include(i));
        return (await query.AsNoTracking().Where(predicate).FirstOrDefaultAsync())!;
    }


    public async Task<bool> Exists(Guid id)
    {
        return await DbSet.AsNoTracking().AnyAsync(x => x.Id == id);
    }
    
    public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().AnyAsync(predicate);
    }

    public void ClearTrackedEntity()
    {
        Db.ChangeTracker.Clear();
    }

    public async Task SaveChanges()
    {
        await Db.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        Dispose(_isDisposed);
    }
    #endregion

    #region Private Methods
    private void Dispose(bool disposing)
    {
        if (_isDisposed) return;
    
        if (disposing)
        {
            Db.Dispose();
        }
        _isDisposed = true;
    }
    #endregion
}

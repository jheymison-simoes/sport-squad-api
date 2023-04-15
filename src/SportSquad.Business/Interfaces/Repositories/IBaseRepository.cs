using System.Linq.Expressions;
using SportSquad.Domain.Models;

namespace SportSquad.Business.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
{
    void Add(TEntity entity);
    void Add(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void Remove(IEnumerable<TEntity> entities);
    Task<ICollection<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
    Task<ICollection<TEntity>> Search(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, Entity>>[] includes);
    Task<TEntity> GetById(Guid id);
    Task<TEntity> GetById(Guid id, params Expression<Func<TEntity, Entity>>[] includes);
    Task<ICollection<TEntity>> GetAll();
    Task<ICollection<TEntity>> GetAll(params Expression<Func<TEntity, Entity>>[] includes);
    Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, Entity>>[] includes);
    Task<bool> Exists(Guid id);
    Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
    void ClearTrackedEntity();
    Task SaveChanges();
}
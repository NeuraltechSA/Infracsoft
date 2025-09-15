using SharedKernel.Domain.Entities;

namespace SharedKernel.Domain.Contracts;

public interface IRepository<TEntity, TId, TCriteria>
    where TEntity : Entity
    where TId : ValueObject<string>
    where TCriteria : class
{
    Task Create(TEntity entity);
    Task Update(TEntity entity);
    Task<TEntity?> Find(TId id);
    Task<IEnumerable<TEntity>> Find(TCriteria criteria);
    Task Delete(TEntity entity);
}
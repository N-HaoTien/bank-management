namespace Bank.Management.Domain;

public interface IRepository<T, TId> where T : EntityBase<TId>
{
    Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<T> GetOneBySpecification(SpecificationBase<T, TId> spec, CancellationToken cancellationToken = default);
    Task<List<T>> GetListBySpecification(SpecificationBase<T, TId> spec, CancellationToken cancellationToken = default);

    Task<TId> CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}